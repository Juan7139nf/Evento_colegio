import React, { useEffect, useState } from "react";
import {
  Container,
  Row,
  Col,
  Card,
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
import "./Dashboard.css"; // 👈 Importar estilos personalizados

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
      <div className="dashboard-loading">
        <Spinner animation="border" variant="primary" />
      </div>
    );
  }

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
          "#4682B4", // Azul acero
          "#fadc16ff", 
          "#14e2e9ff",
          "#d93848ff",
          "#542ade9e",
        ],
        borderRadius: 8,
      },
    ],
  };

  const pieData = {
    labels: ["Completado", "Pendiente"],
    datasets: [
      {
        data: [data.inscripciones.Completado, data.inscripciones.Pendiente],
        backgroundColor: ["#2E8B57", "#808080"],
        borderWidth: 2,
      },
    ],
  };

  return (
    <Container fluid className="dashboard-container">
      <h1 className="text-center dashboard-title">Registrados</h1>

      {/* Cards de estadísticas rápidas */}
      <Row className="mb-5 g-4 justify-content-center">
        <Col md={3}>
          <Card bg="primary" text="white" className="text-center dashboard-card">
            <Card.Body>
              <Card.Title>Eventos</Card.Title>
              <Card.Text>{data.totalEventos}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="warning" text="white" className="text-center dashboard-card">
            <Card.Body>
              <Card.Title>Inscripciones</Card.Title>
              <Card.Text>{data.totalInscripciones}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="info" text="white" className="text-center dashboard-card">
            <Card.Body>
              <Card.Title>Usuarios</Card.Title>
              <Card.Text>{data.totalUsuarios}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="danger" text="white" className="text-center dashboard-card">
            <Card.Body>
              <Card.Title>Pagos</Card.Title>
              <Card.Text>{data.totalPagos}</Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Gráficas */}
      <Row className="d-flex align-items-stretch">
        <Col md={7} className="mb-4">
          <Card className="dashboard-chart h-100">
            <Card.Body>
              <Card.Title>Resumen General</Card.Title>
              <Bar data={barData} />
            </Card.Body>
          </Card>
        </Col>
        <Col md={5} className="mb-4">
          <Card className="dashboard-chart h-100">
            <Card.Body>
              <Card.Title>Estado de Inscripciones</Card.Title>
              <Pie data={pieData} />
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
