import { Nav, NavLink, NavMenu } from "./NavbarElements";

function Navbar() {
    return (
        <>
        <Nav>
            <NavMenu>
                <NavLink to = "/Home"> Home </NavLink>
                <NavLink to = "/ResumeLists"> List of Resumes </NavLink>
            </NavMenu>
        </Nav>
        </>
    );
}

export default Navbar;