import React, { useContext } from "react";
import { signOut } from "firebase/auth";
import { auth } from "../../services/firebaseConfig";
import { UserContext } from "../../contexts/UserContext";

export default function Logout() {
const { logout } = useContext(UserContext);

  const handleLogout = async () => {
    await signOut(auth);
    console.log("Sesión cerrada");
    logout();
  };

  return <button onClick={handleLogout}>Cerrar sesión</button>;
}
