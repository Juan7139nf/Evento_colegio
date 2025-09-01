import React, { useContext, useEffect, useState } from "react";
import {
  Button,
  Card,
  Col,
  Container,
  Modal,
  Row,
  Spinner,
} from "react-bootstrap";
import { NavLink, useNavigate, useParams } from "react-router-dom";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { UserContext } from "../contexts/UserContext";
import Login from "./auth/Login";
import Encuesta from "../components/Encuesta";

export default function EventoShow() {
  const { id } = useParams();
  const { usuario, login } = useContext(UserContext);

  const navigate = useNavigate();
  const [evento, setEvento] = useState({});
  const [inscripcion, setInscripcion] = useState([]);
  const [pago, setPago] = useState([]);
  const [encuesta, setEncuesta] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showPago, setShowPago] = useState(false);
  const [showEncuesta, setShowEncuesta] = useState(false);

  useEffect(() => {
    const fetchEvento = async () => {
      const dataUser = JSON.parse(localStorage.getItem("usuario"));
      login(dataUser);
      try {
        if (dataUser) {
          const response = await fetch(
            `https://localhost:7294/api/DetalleEvento/${id}/{${dataUser.id}}`
          );
          if (!response.ok) throw new Error("Error al cargar evento");
          const data = await response.json();
          setEvento({
            id: data.evento.id,
            nombre_Evento: data.evento.nombre_Evento || "",
            tipo: data.evento.tipo || "",
            fecha: data.evento.fecha ? data.evento.fecha.slice(0, 16) : "",
            lugar: data.evento.lugar || "",
            capacidad_Max: data.evento.capacidad_Max || 1,
            estado: data.evento.estado || "Activo",
            encuesta: data.evento.encuesta
              ? JSON.parse(data.evento.encuesta)
              : [],
            costo: data.evento.costo || "",
            content: data.evento.content || [],
            archivos: data.evento.archivos || [],
          });
          if (data.inscripcion) setInscripcion(data.inscripcion);
          if (data.pago) setPago(data.pago);
          if (data.encuesta) setEncuesta(data.encuesta);
        } else {
          const response = await fetch(
            `https://localhost:7294/api/Eventos/${id}`
          );
          if (!response.ok) throw new Error("Error al cargar evento");
          const data = await response.json();
          setEvento({
            id: data.id,
            nombre_Evento: data.nombre_Evento || "",
            tipo: data.tipo || "",
            fecha: data.fecha ? data.fecha.slice(0, 16) : "",
            lugar: data.lugar || "",
            capacidad_Max: data.capacidad_Max || 1,
            estado: data.estado || "Activo",
            encuesta: data.encuesta ? JSON.parse(data.encuesta) : [],
            costo: data.costo || "",
            content: data.content || [],
            archivos: data.archivos || [],
          });
        }
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };

    fetchEvento();
  }, [id]);

  const handleInscribir = async () => {
    const payload = {
      fecha_Inscripcion: new Date().toISOString(),
      estado: "Pendiente",
      id_Usuario: usuario.id,
      id_Evento: evento.id,
    };

    try {
      const response = await fetch("https://localhost:7294/api/Inscripciones", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (response.ok) {
        const data = await response.json();
        alert(data.message);
        setInscripcion(data.data);
        setShowPago(true);
      } else {
        alert("Error al registrar la inscripción ❌");
      }
    } catch (error) {
      console.error("Error en la solicitud:", error);
    }
  };

  const handleConfirmPago = async () => {
    const payload = {
      id_Inscripcion: inscripcion.id,
      fecha_Inscripcion: inscripcion.fecha_Inscripcion,
      estado: "Completado",
      id_Usuario: inscripcion.id_Usuario,
      id_Evento: inscripcion.id_Evento,
    };
    const payloapdPago = {
      fecha_Pago: new Date().toISOString(),
      monto: evento.costo,
      metodo: "Web",
      estado: "Completado",
      id_Inscripcion: inscripcion.id,
    };

    try {
      const response = await fetch("https://localhost:7294/api/Pagos", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payloapdPago),
      });
      if (response.ok) {
        await response.json();
        alert("Se ha realizado el pago con éxito ✅");
        setPago(payloapdPago);
        setShowPago(false);
      } else {
        alert("Error al registrar el pago ❌");
      }
    } catch (error) {
      console.error("Error en la solicitud:", error);
    }
  };

  if (loading)
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" />
      </div>
    );

  return (
    <Container fluid="sm" className="mt-4 mb-5">
      <div className="d-flex justify-content-end align-items-center">
        <Button
          onClick={() => navigate(-1)}
          className="btn-custom-secondary shadow-sm"
        >
          Volver
        </Button>
      </div>

      <h1 className="fw-bold text-center text-gradient my-3">
        {evento.nombre_Evento}
      </h1>

      <Card className="p-3 my-3 shadow card-custom">
        <p><strong>Tipo:</strong> {evento.tipo}</p>
        <p>
          <strong>Fecha:</strong>{" "}
          {format(new Date(evento.fecha), "dd 'de' MMMM 'de' yyyy, HH:mm", {
            locale: es,
          })}
        </p>
        <p><strong>Lugar:</strong> {evento.lugar}</p>
        <p><strong>Capacidad Máxima:</strong> {evento.capacidad_Max}</p>
        <p><strong>Costo:</strong> {evento.costo} Bs</p>
        {inscripcion && (
          <p>
            <strong>Estado de Inscripción:</strong> {inscripcion.estado}
          </p>
        )}
      </Card>

      {evento.estado === "Activo" && (
        <div className="text-center mb-4">
          {usuario ? (
            <>
              {inscripcion.length === 0 ? (
                <Button className="btn-custom-green" onClick={handleInscribir}>
                  Deseas Inscribirte?
                </Button>
              ) : pago.length === 0 ? (
                <Button
                  className="btn-custom-blue"
                  onClick={() => setShowPago(true)}
                >
                  Realizar Pago
                </Button>
              ) : (
                inscripcion.estado === "Completado" && (
                  <Button
                    className="btn-custom-blue"
                    onClick={() => setShowEncuesta(true)}
                  >
                    {encuesta.length === 0
                      ? "Deseas Responder la Encuesta?"
                      : "Modificar Respuesta"}
                  </Button>
                )
              )}
            </>
          ) : (
            <Login text="Inicia sesión para inscribirte en el evento" />
          )}
        </div>
      )}

      {/* Modal de Pago */}
      {showPago && (
        <Modal show={showPago} onHide={() => setShowPago(false)} centered>
          <Modal.Header closeButton className="bg-custom-blue text-white">
            <Modal.Title>Realizar Pago</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <p>¿Estás seguro de que deseas realizar el pago?</p>
            <p>
              Este es un pago de <strong>{evento.costo} Bs</strong>
            </p>
          </Modal.Body>
          <Modal.Footer>
            <Button
              className="btn-custom-secondary"
              onClick={() => setShowPago(false)}
            >
              Cancelar
            </Button>
            <Button className="btn-custom-blue" onClick={handleConfirmPago}>
              Confirmar Pago
            </Button>
          </Modal.Footer>
        </Modal>
      )}

      {showEncuesta && (
        <Encuesta
          dataEncuesta={encuesta}
          encuesta={encuesta.value}
          setEncuesta={setEncuesta}
          showEncuesta={showEncuesta}
          setShowEncuesta={setShowEncuesta}
          data={evento.encuesta}
          id_Evento={evento.id}
          id_Inscripcion={inscripcion.id}
        />
      )}

      {/* Galería */}
      <Row className="mt-4">
        {evento.archivos?.map((archivo) => (
          <Col key={archivo.orden} xs={12} sm={6} lg={4} className="mb-4">
            <Card className="h-100 shadow-sm card-hover">
              <Card.Img
                src={archivo.url || "https://via.placeholder.com/300"}
                alt={evento.nombre_Evento}
                className="object-fit-cover h-100"
              />
            </Card>
          </Col>
        ))}
      </Row>

      {/* Secciones de contenido */}
      {evento.content?.map((section, idx) => (
        <Card key={idx} className="p-3 my-3 shadow card-custom">
          <h3 className="fw-bold">{section.title}</h3>
          <p>{section.content}</p>
        </Card>
      ))}

      {/* Estilos personalizados */}
      <style>
        {`
          .text-gradient {
            background: linear-gradient(90deg, #4682B4, #00008B);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
          }
          .btn-custom-blue {
            background-color: #4682B4;
            border: none;
            color: #fff;
          }
          .btn-custom-blue:hover {
            background-color: #00008B;
          }
          .btn-custom-green {
            background-color: #2E8B57;
            border: none;
            color: #fff;
          }
          .btn-custom-green:hover {
            background-color: #ACF6C8;
            color: #000;
          }
          .btn-custom-secondary {
            background-color: #808080;
            border: none;
            color: #fff;
          }
          .btn-custom-secondary:hover {
            background-color: #D3D3D3;
            color: #000;
          }
          .card-custom {
            border-radius: 12px;
            background-color: #fff;
          }
          .card-hover {
            transition: transform 0.3s ease, box-shadow 0.3s ease;
          }
          .card-hover:hover {
            transform: translateY(-5px);
            box-shadow: 0px 8px 20px rgba(0,0,0,0.2);
          }
          .bg-custom-blue {
            background-color: #4682B4 !important;
          }
        `}
      </style>
    </Container>
  );
}
