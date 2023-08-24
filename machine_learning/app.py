# Appending the path of the root folder to the system path
import sys
import os
sys.path.append(os.path.abspath("/machine_learning"))

from flask import Flask, request
import torch

from API.pdf_to_text import pdf_to_text
from llm.create_prompt import create_prompt
from llm.model_gpu import run_gpu_model
from llm.model_cpu import run_cpu_model
from llm.extract_json import extract_json


app = Flask(__name__)


@app.route("/upload", methods = ["POST"])
def uploadedFile():
    resume_file = request.files["resume"]
    resume_id = request.form.get("resume_id")
    resume_text = pdf_to_text(resume_file)
    prompt = create_prompt(resume_text)
    if (torch.cuda.is_available()):
        output_text = run_gpu_model(prompt)
    else:
        output_text = run_cpu_model(prompt)
    parsed_json = extract_json(output_text, resume_id)
    return parsed_json

app.run()