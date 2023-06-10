import React from 'react';
import "bootstrap/dist/css/bootstrap.min.css";
import FileUpload from '../components/FileUpload';

function Home() {
	return (
		<div className="container" style={{ width: "600px" }}>
			<div className="my-3">
			<h1>Resume Parser</h1>
			<h1>--------------------------------</h1>
			<h4>Please upload your resume here - </h4>
			</div>

			<FileUpload />
		</div>
	);
}

export default Home;
