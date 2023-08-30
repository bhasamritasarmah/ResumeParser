import { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDownload } from '@fortawesome/free-solid-svg-icons';

const listResumeURL = process.env.REACT_APP_LIST_RESUME_URL;
const downloadResumeURL = process.env.REACT_APP_DOWNLOAD_RESUME_URL;

interface Experience {
    role: string;
}
interface Resume {
    id: string;
    resume_id: string;
    person_name: string;
    experience: Experience[];
    date_time: string;
}

const baseURL = process.env.REACT_APP_BASE_URL;

function ListOfResumes() {
    const [resumes, setResumes] = useState<Resume[]>([]);

    useEffect(() => {
        axios.get<Resume[]>(`${baseURL}/resume/listresumes`)
        .then(response => {
            setResumes(response.data);
        })
        .catch(error => {
            console.error("Error fetching resumes: ", error);
        });
    }, []);

    const renderResumeList = () => {
        return (
            <tbody>
                {resumes.map(resume => (
                    <tr key={ resume.id }>
                        <td>
                            <Link to={`/ResumeDetails/${resume.id}`} title="Parsed Details">
                                { resume.person_name } ( { resume.experience[0].role } )
                            </Link>
                        </td>
                        <td>
                            |
                        </td>
                        <td>
                            Created on { resume.date_time }
                        </td>
                        <td>
                            |
                        </td>
                        <td>
                            <Link to={`/RawResume/${resume.id}`}>
                                View Raw File
                            </Link>
                        </td>
                        <td>
                            |
                        </td>
                        <td>
                            <a href={`${baseURL}/resume/downloadresume?id=${resume.resume_id}`} title="Download Resume">
                                <FontAwesomeIcon icon={ faDownload } />
                            </a>
                        </td>
                    </tr>
                ))}
            </tbody>
        )
    }

	return (
		<div className="ListOfResumes">
			<h3>List of all the parsed resumes: </h3>
            { renderResumeList() }
		</div>
	);
}

export default ListOfResumes;