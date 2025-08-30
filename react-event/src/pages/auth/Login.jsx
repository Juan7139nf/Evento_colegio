import { signInWithPopup } from "firebase/auth";
import React, { useContext } from "react";
import { auth, googleProvider } from "../../services/firebaseConfig";
import { Button, Container } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { UserContext } from "../../contexts/UserContext";

export default function Login() {
  const navigate = useNavigate();
  const { login } = useContext(UserContext);

  const loginGoogle = async () => {
    try {
      const result = await signInWithPopup(auth, googleProvider);
      const user = result.user;

      console.log("Usuario:", user);

      const usuario = {
        token: user.uid,
        correo: user.email,
        displayName: user.displayName,
        photoURL: user.photoURL,
      };
      localStorage.setItem("usuario", JSON.stringify(usuario));

      const response = await fetch("https://localhost:7294/api/Auth/user", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ correo: user.email }),
      });

      const data = await response.json();
      console.log("Respuesta del servidor:", data);

      if (data.success) {
        console.log("Login exitoso:", data);
        const userLocal = {
          id: data.data.id,
          nombre: data.data.nombre,
          apellido: data.data.apellido,
          token: data.data.token,
          correo: data.data.correo,
          rol: data.data.rol,
          displayName: usuario.displayName,
          photoURL: usuario.photoURL,
        };
        login(userLocal);
        navigate("/");
      } else {
        console.log("Usuario no existe:", data.message);
        navigate("/register");
      }
    } catch (error) {
      console.error("Error en login:", error);
    }
  };
  /* 
  const handleLogin = async () => {
    try {
      const response = await fetch("https://localhost:7294/api/Auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          correo: form.correo,
          contrasenia: form.contrasenia,
        }),
      });

      const data = await response.json();

      if (!response.ok || !data.token) {
        setError(data.mensaje || "Correo o contraseña incorrectos");
        return;
      }

      // Login exitoso
      localStorage.setItem("usuario", JSON.stringify(data.usuario));
      console.log("Usuario logueado:", data.usuario);
      setSuccess(data.mensaje || "Login correcto");

      // Redirigir
      window.location.href = "/";
    } catch (err) {
      setError("Hubo un problema con el servidor");
    }
  }; */

  return <Button onClick={loginGoogle} >Iniciar sesión</Button>;
}

  /* {<Container}
      fluid="sm"
      className="d-flex justify-content-center align-items-center vh-100"
    >
      <div className="text-center">
        <h1>Iniciar sesión</h1>{/*  */

  /* </div>
    </Container> */
