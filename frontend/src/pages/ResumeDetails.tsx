import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

interface Education {
    institute_name: string;
    degree: string;
    major: string;
    start_date: string;
    end_date: string;
}
interface Experience {
    company_name: string;
    role: string;
    start_date: string;
    end_date: string;
    responsibilities: string;
}
interface Project {
    project_name: string;
    start_date: string;
    end_date: string;
    responsibilities: string;
}
interface Skills {
    technical_skills: string[];
    soft_skills: string[];
    other_skills: string[];
}
interface Resume {
    id: string;
    resume_id: string;
    person_name: string;
    phone_number: string;
    email: string;
    education: Education[];
    experience: Experience[];
    project: Project[];
    skills: Skills;
}

function ResumeDetails() {  
    const { id } = useParams<{ id: string }>();
    const [resumeDetail, setResumeDetail] = useState<Resume | null>(null);

    useEffect(() => {
        axios.get<Resume>(`https://localhost:7173/api/resume/resumedetails?id=${id}`)
        .then(response => {
            setResumeDetail(response.data);
        })
        .catch(error => {
            console.error("Error fetching resume details: ", error);
        });
    }, [id]);

    if (!resumeDetail){
        return (
            <div>
                Loading...
            </div>
        );
    }

	return (
		<div className="ResumeDetails">
            <tbody>
                <div>
                    <tr>
                        <th>Name: </th>   <td>{ resumeDetail.person_name }</td>
                    </tr>
                    <tr>
                        <th>Phone Number: </th>     <td>{ resumeDetail.phone_number }</td>
                    </tr>
                    <tr>
                        <th>Email ID: </th>     <td>{ resumeDetail.email }</td>
                    </tr>
                </div>
                { resumeDetail.education.map((edu, index) =>
                    <div key={index}>
                        <tr>
                            <th>Education: </th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Institute Name: </th>   <td>{ edu.institute_name }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Degree: </th>       <td>{ edu.degree }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Major: </th>       <td>{ edu.major }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Start Date: </th>       <td>{ edu.start_date }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>End Date: </th>       <td>{ edu.end_date }</td>
                        </tr>
                    </div>
                )}
                { resumeDetail.experience.map((exp, index) =>
                    <div key={index}>
                        <tr>
                            <th>Experience: </th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Company Name: </th>   <td>{ exp.company_name }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Role: </th>       <td>{ exp.role }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Start Date: </th>       <td>{ exp.start_date }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>End Date: </th>       <td>{ exp.end_date }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Responsibilities: </th>       <td>{ exp.responsibilities }</td>
                        </tr>
                    </div>
                )}
                { resumeDetail.project.map((proj, index) =>
                    <div key={index}>
                        <tr>
                            <th>Project: </th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Project Name: </th>   <td>{ proj.project_name }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Start Date: </th>       <td>{ proj.start_date }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>End Date: </th>       <td>{ proj.end_date }</td>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Responsibilities: </th>       <td>{ proj.responsibilities }</td>
                        </tr>
                    </div>
                )}
                <div>
                    <tr>
                        <th>Skills:</th>
                    </tr>
                    <tr>
                        <th></th>
                        <th>Technical Skills: </th>   <td>{ resumeDetail.skills.technical_skills.join(", ") }</td>
                    </tr>
                    <tr></tr>
                    <tr>
                        <th></th>
                        <th>Soft Skills: </th>   <td>{ resumeDetail.skills.soft_skills.join(", ") }</td>
                    </tr>
                    <tr>
                        <th></th>
                        <th>Other Skills: </th>   <td>{ resumeDetail.skills.other_skills.join(", ") }</td>
                    </tr>
                </div>
            </tbody>
		</div>
	);
}

export default ResumeDetails;