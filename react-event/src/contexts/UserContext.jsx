import React, { createContext, useState, useEffect } from "react";

// Crear el contexto
export const UserContext = createContext();

// Provider del contexto
export const UserProvider = ({ children }) => {
  const [usuario, setUsuario] = useState(null);

  // Al montar, leer localStorage
  useEffect(() => {
    const storedUser = localStorage.getItem("usuario");
    if (storedUser) setUsuario(JSON.parse(storedUser));
  }, []);

  // Función para actualizar usuario y localStorage
  const login = (userData) => {
    localStorage.setItem("usuario", JSON.stringify(userData));
    setUsuario(userData);
  };

  // Función para limpiar usuario (logout)
  const logout = () => {
    localStorage.removeItem("usuario");
    setUsuario(null);
  };

  return (
    <UserContext.Provider value={{ usuario, login, logout }}>
      {children}
    </UserContext.Provider>
  );
};
