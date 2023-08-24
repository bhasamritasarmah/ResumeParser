# The following import is required to convert PDF to Text
from pypdf import PdfReader


def pdf_to_text(resume_file):
    resume = PdfReader(resume_file)
    num_pages = len(resume.pages)

    resume_text = ""
    for i in range(num_pages):
        page = resume.pages[i]
        resume_text += " " + page.extract_text()

    return resume_text