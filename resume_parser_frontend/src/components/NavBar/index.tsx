import { Nav, NavLink, NavMenu } from "./NavbarElements";

function Navbar() {
    return (
        <>
        <Nav>
            <NavMenu>
                <NavLink to = "/Home"> Home </NavLink>
                <NavLink to = "/ListOfResumes"> List of Resumes </NavLink>
                <NavLink to = "/ResumeDetails"> Resume Details </NavLink>
            </NavMenu>
        </Nav>
        </>
    );
}

export default Navbar;