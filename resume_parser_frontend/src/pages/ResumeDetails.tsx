import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

interface Resume {
    id: string;
    resume_id: string;
    person_name: string;
    phone_number: string;
    email: string;
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
                <tr>
                    <th>Name: </th>   <td>{ resumeDetail.person_name }</td>
                </tr>
                <tr>
                    <th>Phone Number: </th>     <td>{ resumeDetail.phone_number }</td>
                </tr>
                <tr>
                    <th>Email ID: </th>     <td>{ resumeDetail.email }</td>
                </tr>
            </tbody>
		</div>
	);
}

export default ResumeDetails;