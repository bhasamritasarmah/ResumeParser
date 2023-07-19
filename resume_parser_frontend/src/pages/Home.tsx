import "bootstrap/dist/css/bootstrap.min.css";
import FileUpload from '../components/FileUpload';

/* The function Home implements the Home page which is used to upload the
 * resume files. It displays a few simple HTML text and implements the
 * react component called 'FileUpload'. */
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
