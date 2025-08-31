import { set } from "date-fns";
import { id, se } from "date-fns/locale";
import React, { useState, useEffect } from "react";
import { Button, Modal, Form } from "react-bootstrap";

export default function Encuesta({
  dataEncuesta,
  encuesta,
  setEncuesta,
  data,
  showEncuesta,
  setShowEncuesta,
  id_Evento,
  id_Inscripcion,
}) {
  // formRespuesta contendrá las respuestas
  const [formRespuesta, setFormRespuesta] = useState({});

  // Inicializar respuestas con valores vacíos
  useEffect(() => {
    const inicial = [];
    if (dataEncuesta.length > 0) {
      const resp = JSON.parse(encuesta?.value) || [];
      data.forEach((dato, index) => {
        inicial[index] = {
          id: index,
          value: resp[index]?.value || "",
          type: dato.tipo,
        };
      });
    } else {
      data.forEach((dato, index) => {
        inicial[index] = {
          id: index,
          value: "",
          type: dato.tipo,
        };
      });
    }
    setFormRespuesta(inicial);
  }, [data]);

  const handleChange = (index, value) => {
    setFormRespuesta((prev) => ({
      ...prev,
      [index]: { ...prev[index], value }, // solo actualizamos el value
    }));
  };

  // Enviar encuesta al backend
  const handleSubmit = async () => {
    const respuestasArray = Object.values(formRespuesta); // convierte el objeto a array

    if (dataEncuesta.length > 0) {
      const payload = {
        id: dataEncuesta.id,
        fecha_Creacion: dataEncuesta.fecha_Creacion,
        value: JSON.stringify(respuestasArray),
        id_Evento,
        id_Inscripcion,
      };

      console.log("Payload de encuesta:", payload);

      try {
        const response = await fetch(
          `https://localhost:7294/api/Encuestas/${dataEncuesta.id}`,
          {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload),
          }
        );

        if (response.ok) {
          alert("Encuesta Modificada");
          setShowEncuesta(false);
          setEncuesta(payload); // Actualiza la encuesta en el componente padre
          console.log("Encuesta enviada:", response);
        } else {
          alert("Error al enviar la encuesta ❌");
        }
      } catch (err) {
        console.error(err);
        alert("Error de conexión con el servidor 🚨");
      }
    } else {
      const payload = {
        fecha_Creacion: new Date().toISOString(),
        value: JSON.stringify(respuestasArray), // usamos formRespuesta
        id_Evento,
        id_Inscripcion,
      };

      console.log("Payload de encuesta:", payload);

      try {
        const response = await fetch("https://localhost:7294/api/Encuestas", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(payload),
        });

        if (response.ok) {
          const data = await response.json();
          alert("Encuesta enviada");
          setShowEncuesta(false);
          console.log("Encuesta enviada:", response);
          setEncuesta(data); // Actualiza la encuesta en el componente padre
        } else {
          alert("Error al enviar la encuesta ❌");
        }
      } catch (err) {
        console.error(err);
        alert("Error de conexión con el servidor 🚨");
      }
    }
  };

  return (
    <Modal
      show={showEncuesta}
      onHide={() => setShowEncuesta(false)}
      backdrop="static"
      keyboard={false}
    >
      {console.log(data)}
      <Modal.Header closeButton>
        <Modal.Title>
          {dataEncuesta.length === 0
            ? "Realizar Encuesta"
            : "Modificar Encuesta"}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {data.map((pregunta, index) => (
          <Form.Group className="mb-3" key={index}>
            <Form.Label>{pregunta.pregunta}</Form.Label>

            {pregunta.tipo === "texto" && (
              <Form.Control
                type="text"
                value={formRespuesta[index]?.value || ""}
                onChange={(e) => handleChange(index, e.target.value)}
                placeholder="Escribe tu respuesta aquí"
              />
            )}

            {pregunta.tipo === "numero" && (
              <Form.Control
                type="number"
                value={formRespuesta[index]?.value || ""}
                onChange={(e) => handleChange(index, e.target.value)}
              />
            )}

            {pregunta.tipo === "si/no" && (
              <Form.Select
                value={formRespuesta[index]?.value || ""}
                onChange={(e) => handleChange(index, e.target.value)}
              >
                <option value="">Selecciona...</option>
                <option value="sí">Sí</option>
                <option value="no">No</option>
              </Form.Select>
            )}

            {pregunta.tipo === "rango" && (
              <Form.Range
                min={1}
                max={5}
                value={formRespuesta[index]?.value || 3}
                onChange={(e) => handleChange(index, e.target.value)}
              />
            )}
          </Form.Group>
        ))}
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={() => setShowEncuesta(false)}>
          Cerrar
        </Button>
        <Button variant="primary" onClick={handleSubmit}>
          {dataEncuesta.length === 0
            ? "Enviar Respuestas"
            : "Modificar Respuestas"}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
