# The following imports are required to load the Falcon-7b LLM model
import transformers
import torch

# The following import is required to convert PDF to Text
from pypdf import PdfReader

# The following import is required to convert Text to JSON
import json

# The following imports are required to run the Flask App
from flask import Flask, render_template, request

# The following import is required to launch the Flask App into a public server using ngrok
from flask_ngrok import run_with_ngrok


# Function to convert the uploaded resume to string format.
def pdf_to_text(resume_file):
    resume = PdfReader(resume_file)
    num_pages = len(resume.pages)

    resume_text = ""
    for i in range(num_pages):
        page = resume.pages[i]
        resume_text += " " + page.extract_text()

    return resume_text


# Function to create the prompt to feed the model, based on the resume text uploaded.
def create_prompt(resume_text):
    instruction = """Use the information provided in the text below to
                  answer the given question as accurately as possible."""

    question = """Find appropriate answers from the text above and compile it into a
               single JSON with exactly the following structure -
               {person_name, phone_number, email}
               Use the exact JSON format as provided.
               Do not eliminate any given key or sub-key from the JSON.
               Do not add any other key or sub-key to the JSON. """

    prompt = instruction + "\n" + "Text: " + resume_text + "\n" + "Question: " + question + "\n" + "Answer:"

    return prompt       


# Function to load the Falcon-7b model into a pipeline
def load_falcon_model():
    model = "tiiuae/falcon-7b"

    tokenizer = transformers.AutoTokenizer.from_pretrained(model)

    pipeline = transformers.pipeline(
        "text-generation",
        model = model,
        tokenizer = tokenizer,
        torch_dtype = torch.bfloat16,
        trust_remote_code = True,
        device_map = "auto"
    )

    return [pipeline, tokenizer]


# Function to run the Falcon-7b model pipeline and to return the output text
def run_falcon_model(pipeline, tokenizer, prompt):
    sequences = pipeline(
        prompt,
        return_full_text = False,
        max_new_tokens = 45,
        #do_sample = True,
        top_k = 1,
        num_return_sequences = 1,
        temperature = 0.0000001,
        eos_token_id = tokenizer.eos_token_id
    )
 
    output_text = ""
    for seq in sequences:
        output_text += seq['generated_text']

    return output_text


# Function to extract the JSON element from the provided output from the model
def extract_json(output_text):
    stack = []
    json_string = ""
    is_first_json = True

    for letter in output_text:
        if not is_first_json:
            break

        if letter == '{':
            stack.append(letter)
        elif letter == '}':     # Assuming JSON has balanced pairs of '{' and '}'
            char = stack.pop()
            if not stack:
                is_first_json = False   #The first JSON string completes here

        if stack:
            json_string += letter

    json_string += '}'
    print(json_string)

    return json_string


# Loading the Falcon-7b model
[pipeline, tokenizer] = load_falcon_model()

# Running the LLM Model in the Flask App
app = Flask(__name__)
app.config["TIMEOUT"] = 900    #15 minutes
run_with_ngrok(app)

@app.route("/")
@app.route("/upload")
def uploadFile():
    return render_template("uploadResume.html")

@app.route("/uploader", methods = ["POST"])
def uploadedFile():
    resume_file = request.files["resume"]
    resume_text = pdf_to_text(resume_file)
    prompt = create_prompt(resume_text)
    output_text = run_falcon_model(pipeline, tokenizer, prompt)
    json_string = extract_json(output_text)

    return json_string


app.run()


