INSTRUCTION = """Use the information provided in the text below to
                 answer the given question as accurately as possible."""

FULL_QUESTION = """Find appropriate answers from the text above and compile it into a
               single JSON with exactly the following structure -
               {person_name, phone_number, email,
                education: {institute_name, degree, major, start_date, end_date},
                experience: {company_name, role, start_date, end_date, responsibilities},
                project: {project_name, start_date, end_date, responsibilities},
                skills: {technical_skills, soft_skills, other_skills}}
               Use the exact JSON format as provided.
               Do not eliminate any given key or sub-key from the JSON.
               Do not add any other key or sub-key to the JSON. """

HALF_QUESTION = """Find appropriate answers from the text above and compile it into a
               single JSON with exactly the following structure -
               {person_name, phone_number, email}
               Use the exact JSON format as provided.
               Do not eliminate any given key or sub-key from the JSON.
               Do not add any other key or sub-key to the JSON. """