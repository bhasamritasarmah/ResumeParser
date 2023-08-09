import { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import ResumeDetails from "./ResumeDetails";

interface Resume {
    id: string;
    resume_id: string;
    person_name: string;
    phone_number: string;
    email: string;
}
function ListOfResumes() {
    const [resumes, setResumes] = useState<Resume[]>([]);

    useEffect(() => {
        axios.get<Resume[]>("https://localhost:7173/api/resume/listresumes")
        .then(response => {
            setResumes(response.data);
        })
        .catch(error => {
            console.error("Error fetching resumes: ", error);
        });
    }, []);

	return (
		<div className="ListOfResumes">
			<h1>List of all the parsed resumes: </h1>
            <ul>
                {resumes.map(resume => (
                    <li key={ resume.id }>
                        <Link to={`/ResumeDetails/${resume.id}`}>
                            { resume.person_name }
                        </Link>
                    </li>
                ))}
            </ul>
		</div>
	);
}

export default ListOfResumes;