import React from "react";
import { Container, Row, Col, Card, ProgressBar } from "react-bootstrap";
import { Bar, Pie } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, ArcElement } from "chart.js";

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, ArcElement);

export default function Dashboard() {
  // Datos de ejemplo para las gráficas
  const barData = {
    labels: ["Eventos", "Inscripciones", "Usuarios", "Pagos"],
    datasets: [
      {
        label: "Cantidad",
        data: [12, 19, 7, 5],
        backgroundColor: ["#007bff", "#28a745", "#ffc107", "#dc3545"],
      },
    ],
  };

  const pieData = {
    labels: ["Completado", "Pendiente", "Cancelado"],
    datasets: [
      {
        data: [10, 5, 2],
        backgroundColor: ["#28a745", "#ffc107", "#dc3545"],
      },
    ],
  };

  return (
    <Container fluid className="mt-4">
      <h1 className="text-center mb-4">Dashboard</h1>

      {/* Cards de estadísticas rápidas */}
      <Row className="mb-4">
        <Col md={3}>
          <Card bg="primary" text="white" className="text-center">
            <Card.Body>
              <Card.Title>Usuarios</Card.Title>
              <Card.Text>120</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="success" text="white" className="text-center">
            <Card.Body>
              <Card.Title>Eventos</Card.Title>
              <Card.Text>25</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="warning" text="dark" className="text-center">
            <Card.Body>
              <Card.Title>Inscripciones</Card.Title>
              <Card.Text>56</Card.Text>
            </Card.Body>
          </Card>
        </Col>
        <Col md={3}>
          <Card bg="danger" text="white" className="text-center">
            <Card.Body>
              <Card.Title>Pagos</Card.Title>
              <Card.Text>40</Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Gráficas */}
      <Row>
        <Col md={6} className="mb-4">
          <Card>
            <Card.Body>
              <Card.Title>Resumen General</Card.Title>
              <Bar data={barData} />
            </Card.Body>
          </Card>
        </Col>
        <Col md={6} className="mb-4">
          <Card>
            <Card.Body>
              <Card.Title>Estado de Inscripciones</Card.Title>
              <Pie data={pieData} />
            </Card.Body>
          </Card>
        </Col>
      </Row>

      {/* Progreso ejemplo */}
      <Row>
        <Col md={12}>
          <Card>
            <Card.Body>
              <Card.Title>Avance de Eventos</Card.Title>
              <ProgressBar now={60} label="60%" />
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}
