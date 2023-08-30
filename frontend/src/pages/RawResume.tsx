import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";

interface Resume {
    id: string;
    raw_resume: string;
}

const baseURL = process.env.REACT_APP_BASE_URL;

function ResumeDetails() {  
    const { id } = useParams<{ id: string }>();
    const [resumeDetail, setResumeDetail] = useState<Resume | null>(null);

    useEffect(() => {
        axios.get<Resume>(`${baseURL}/resume/resumedetails?id=${id}`)
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
		<div className="RawResume">
            { resumeDetail.raw_resume }
		</div>
	);
}

export default ResumeDetails;