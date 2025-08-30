import React, { useContext } from "react";
import { signOut } from "firebase/auth";
import { auth } from "../../services/firebaseConfig";
import { UserContext } from "../../contexts/UserContext";
import { Button } from "react-bootstrap";

export default function Logout() {
const { logout } = useContext(UserContext);

  const handleLogout = async () => {
    await signOut(auth);
    console.log("Sesión cerrada");
    logout();
  };

  return <Button onClick={handleLogout} variant="danger">Cerrar sesión</Button>;
}
