from utilities.prompt_constants import INSTRUCTION, FULL_QUESTION

def create_prompt(resume_text):
    prompt = INSTRUCTION + "\n" + "Text: " + resume_text + "\n" + "Question: " + FULL_QUESTION + "\n" + "Answer:"
    return prompt