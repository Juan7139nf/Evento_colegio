import React, { useEffect, useState } from "react";
import {
  Container,
  Row,
  Col,
  Card,
  ProgressBar,
  Spinner,
} from "react-bootstrap";
import { Bar, Pie } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
  ArcElement,
} from "chart.js";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
  ArcElement
);

export default function Dashboard() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchDashboard = async () => {
      try {
        const response = await fetch("https://localhost:7294/api/Dashboard");
        const data = await response.json();
        setData(data);
      } catch (error) {
        console.error("Error al cargar eventos:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDashboard();
  }, []);

  if (loading) {
    return (
      <div className="d-flex justify-content-center mt-5">
        <Spinner animation="border" />
      </div>
    );
  }

  // Datos de ejemplo para las gráficas
  const barData = {
    labels: ["Eventos", "Inscripciones", "Usuarios", "Pagos", "Encuestas"],
    datasets: [
      {
        label: "Datos",
        data: [
          data.totalEventos,
          data.totalInscripciones,
          data.totalUsuarios,
          data.totalPagos,
          data.totalEncuestas,
        ],
        backgroundColor: [
          "#007bff",
          "#fadc16ff",
          "#14e2e9ff",
          "#d93848ff",
          "#542ade9e",
        ],
      },
    ],
  };

  const pieData = {
    labels: ["Completado", "Pendiente"],
    datasets: [
      {
        data: [data.inscripciones.Completado, data.inscripciones.Pendiente],
        backgroundColor: ["#0ee8a0ff", "#606060ff"],
      },
    ],
  };

  return (
    <Container fluid="sm" className="mt-4">
      {/* Cards de estadísticas rápidas */}
      <h1 className="text-center">Registrados</h1>
      <Row className="mb-4 g-4">
        <Col md={3}>
          <Card bg="primary" text="white" className="text-center bg-opacity-75">
            <Card.Body>
              <Card.Title>Eventos</Card.Title>
              <Card.Text className="fs-2">{data.totalEventos}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="warning" text="white" className="text-center bg-opacity-75">
            <Card.Body>
              <Card.Title>Inscripciones</Card.Title>
              <Card.Text className="fs-2">{data.totalInscripciones}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="info" text="white" className="text-center bg-opacity-75">
            <Card.Body>
              <Card.Title>Usuarios</Card.Title>
              <Card.Text className="fs-2">{data.totalUsuarios}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="danger" text="white" className="text-center bg-opacity-75">
            <Card.Body>
              <Card.Title>Pagos</Card.Title>
              <Card.Text className="fs-2">{data.totalPagos}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Gráficas */}
      <Row className="d-flex align-items-center">
        <Col md={7} className="mb-4">
          <Card>
            <Card.Body>
              <Card.Title>Resumen General</Card.Title>
              <Bar data={barData} />
            </Card.Body>
          </Card>
        </Col>
        <Col md={5} className="mb-4">
          <Card>
            <Card.Body>
              <Card.Title>Estado de Inscripciones</Card.Title>
              <Pie data={pieData} />
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Progreso ejemplo */}
      {/* <Row>
        <Col md={12}>
          <Card>
            <Card.Body>
              <Card.Title>Avance de Eventos</Card.Title>
              <ProgressBar now={60} label="60%" />
            </Card.Body>
          </Card>
        </Col>
      </Row> */}
    </Container>
  );
}
