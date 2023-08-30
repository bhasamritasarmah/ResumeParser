from fileinput import filename
import json
import datetime

# Function to extract the JSON string
def balanced_parentheses(output_text):
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
    return json_string



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
        key_field = [{}]

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



# Function to extract and merge JSONs
def extract_json(output_text, resume_id):
    json_string = balanced_parentheses(output_text)
    try:
        json_element = json.loads(json_string)
    except:
        json_element = ""

    if(json_element != ""):
        person_name = build("person_name", json_element)
        phone_number = build("phone_number", json_element)
        email = build("email", json_element)
        education = build_list("education", json_element)
        experience = build_list("experience", json_element)
        project = build_list("project", json_element)
        technical_skills = build_skill("technical_skills", json_element)
        soft_skills = build_skill("soft_skills", json_element)
        other_skills = build_skill("other_skills", json_element)
    else:
        person_name = ""
        phone_number = ""
        email = ""
        education = [{}]
        experience = [{}]
        project = [{}]
        technical_skills = []
        soft_skills = []
        other_skills = []

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


