from pypdf import PdfReader
from text_generation import InferenceAPIClient
import json
import datetime
from flask import Flask, request
import docx


# Function to convert the uploaded resume to string format.
def pdf_to_text(resume_file):
    resume = PdfReader(resume_file)
    num_pages = len(resume.pages)

    resume_text = ""
    for i in range(num_pages):
        page = resume.pages[i]
        resume_text += " " + page.extract_text()

    return resume_text


def docx_to_text(resume_file):
    doc = docx.Document(resume_file)
    content = []

    for paragraph in doc.paragraphs:
        content.append(paragraph.text)

    return "\n".join(content)


def read_text(resume_file):
    content = resume_file.read().decode("utf-8")
    return content


# Function to create the prompt to feed the model, based on the resume text uploaded.
def create_prompt(resume_text):
    instruction = """Use the information provided in the text below to
                  answer the given question as accurately as possible."""

    question = """Find appropriate answers from the text above and compile it into a
               single JSON with exactly the following structure -
               {person_name, phone_number, email,
                education: {institute_name, degree, major, start_date, end_date},
                experience: {company_name, role, start_date, end_date, responsibilities},
                project: {project_name, start_date, end_date, responsibilities},
                skills: {technical_skills, soft_skills, other_skills}}
               Use the exact JSON format as provided.
               Do not eliminate any given key or sub-key from the JSON.
               Do not add any other key or sub-key to the JSON. """

    prompt = instruction + "\n" + "Text: " + resume_text + "\n" + "Question: " + question + "\n" + "Answer:"

    return prompt       


# Function to call the llm model client
timeout_seconds = 60
client = InferenceAPIClient("tiiuae/falcon-7b", timeout=timeout_seconds)


# Function to extract and merge JSONs
def extract_json(output_text, resume_id):
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

    json_element = json.loads(json_string)
    person_name = build("person_name", json_element)
    phone_number = build("phone_number", json_element)
    email = build("email", json_element)
    education = build_list("education", json_element)
    experience = build_list("experience", json_element)
    project = build_list("project", json_element)
    technical_skills = build_skill("technical_skills", json_element)
    soft_skills = build_skill("soft_skills", json_element)
    other_skills = build_skill("other_skills", json_element)

    parsed_json = {
        "resume_id": resume_id,
        "person_name": person_name,
        "phone_number": phone_number,
        "email": email,
        "education": education,
        "experience": experience,
        "project": project,
        "skills": {
            "technical_skills": technical_skills,
            "soft_skills": soft_skills,
            "other_skills": other_skills,
        },
        "raw_resume": output_text,
        "date_time": str(datetime.datetime.now().replace(microsecond=0)),
    }
    return str(parsed_json)


# Functions to check the structure of JSON file
def build(key_name, parsed_json):
    try:
        if (isinstance(parsed_json[key_name], str)):
            key_field = parsed_json[key_name]
        else:
            key_filed = ""
    except:
        key_field = ""

    return key_field


def build_list(key_name, parsed_json):
    try:
        if (isinstance(parsed_json[key_name], list)):
            key_field = parsed_json[key_name]
        else:
            key_field = []
            key_field.append(parsed_json[key_name])
    except:
        key_field = []

    return key_field


def build_skill(key_name, parsed_json):
    skills = "skills"
    try:
        if (isinstance(parsed_json[skills][key_name], list)):
            key_field = parsed_json[skills][key_name]
        elif (isinstance(parsed_json[skills][key_name], str)):
            key_field = []
            key_field.append(parsed_json[skills][key_name])
    except:
        key_field = []

    return key_field



# Running the LLM Model in the Flask App
app = Flask(__name__)

@app.route("/upload", methods = ["POST"])
def uploadedFile():
    resume_file = request.files["resume"]
    resume_id = request.form.get("resume_id")

    file_extension = resume_file.filename.rsplit('.', 1)[1].lower()
    if (file_extension == "pdf"):
        resume_text = pdf_to_text(resume_file)
    if (file_extension == "docx"):
        resume_text = docx_to_text(resume_file)
    if (file_extension == "txt"):
        resume_text = read_text(resume_file)

    prompt = create_prompt(resume_text)
    output_text = client.generate(prompt, max_new_tokens=550, temperature=0.001).generated_text
    parsed_json = extract_json(output_text, resume_id)
    return parsed_json


app.run()



