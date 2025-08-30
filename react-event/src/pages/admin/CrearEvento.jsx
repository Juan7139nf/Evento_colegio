import React, { useState } from "react";
import {
  Button,
  Col,
  Container,
  Form,
  Row,
  InputGroup,
  ButtonGroup,
  Image,
} from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function CrearEvento() {
  const [formData, setFormData] = useState({
    nombre_Evento: "",
    tipo: "",
    fecha: "",
    lugar: "",
    capacidad_Max: 1,
    estado: "Activo",
    encuesta: [],
    costo: "",
    content: [],
    archivos: [],
  });
  const navigate = useNavigate();

  const [preguntaTemp, setPreguntaTemp] = useState({
    pregunta: "",
    tipo: "concurso",
    editIndex: null,
  });

  const [contentTemp, setContentTemp] = useState({
    title: "",
    content: "",
    editIndex: null,
  });

  const [archivoTemp, setArchivoTemp] = useState({
    url: "",
    tipo: "",
    editIndex: null,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handlePreguntaChange = (e) => {
    const { name, value } = e.target;
    setPreguntaTemp((prev) => ({ ...prev, [name]: value }));
  };

  const handleContentChange = (e) => {
    const { name, value } = e.target;
    setContentTemp((prev) => ({ ...prev, [name]: value }));
  };
  // Funciones generales para agregar/editar/eliminar
  const agregarPregunta = () => {
    if (preguntaTemp.editIndex !== null) {
      const nuevaEncuesta = [...formData.encuesta];
      nuevaEncuesta[preguntaTemp.editIndex] = {
        order: preguntaTemp.editIndex,
        pregunta: preguntaTemp.pregunta,
        tipo: preguntaTemp.tipo,
      };
      setFormData((prev) => ({ ...prev, encuesta: nuevaEncuesta }));
    } else {
      setFormData((prev) => ({
        ...prev,
        encuesta: [
          ...prev.encuesta,
          {
            order: prev.encuesta.length,
            pregunta: preguntaTemp.pregunta,
            tipo: preguntaTemp.tipo,
          },
        ],
      }));
    }
    setPreguntaTemp({ pregunta: "", tipo: "concurso", editIndex: null });
  };

  const editarPregunta = (index) => {
    const p = formData.encuesta[index];
    setPreguntaTemp({ pregunta: p.pregunta, tipo: p.tipo, editIndex: index });
  };

  const eliminarPregunta = (index) => {
    const nuevaEncuesta = formData.encuesta
      .filter((_, i) => i !== index)
      .map((p, i) => ({ ...p, order: i }));
    setFormData((prev) => ({ ...prev, encuesta: nuevaEncuesta }));
  };

  const agregarContent = () => {
    if (contentTemp.editIndex !== null) {
      const newContent = [...formData.content];
      newContent[contentTemp.editIndex] = {
        orden: contentTemp.editIndex,
        title: contentTemp.title,
        content: contentTemp.content,
      };
      setFormData((prev) => ({ ...prev, content: newContent }));
    } else {
      setFormData((prev) => ({
        ...prev,
        content: [
          ...prev.content,
          {
            orden: prev.content.length,
            title: contentTemp.title,
            content: contentTemp.content,
          },
        ],
      }));
    }
    setContentTemp({ title: "", content: "", editIndex: null });
  };

  const editarContent = (index) => {
    const c = formData.content[index];
    setContentTemp({ title: c.title, content: c.content, editIndex: index });
  };

  const eliminarContent = (index) => {
    const newContent = formData.content
      .filter((_, i) => i !== index)
      .map((c, i) => ({ ...c, orden: i }));
    setFormData((prev) => ({ ...prev, content: newContent }));
  };

  const editarArchivo = (index) => {
    const a = formData.archivos[index];
    setArchivoTemp({ url: a.url, tipo: a.tipo, editIndex: index });
  };

  const eliminarArchivo = (index) => {
    const newArchivos = formData.archivos
      .filter((_, i) => i !== index)
      .map((a, i) => ({ ...a, orden: i }));
    setFormData((prev) => ({ ...prev, archivos: newArchivos }));
  };

  // Fecha mínima: mañana
  const minDate = new Date();
  minDate.setDate(minDate.getDate() + 1);
  const minDateStr = minDate.toISOString().slice(0, 16);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const payload = {
      ...formData,
      encuesta: JSON.stringify(formData.encuesta),
      content: formData.content,
      archivos: formData.archivos,
    };

    
  try {
    const response = await fetch("https://localhost:7294/api/Eventos", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      const errorData = await response.json();
      console.error("Error al crear evento:", errorData);
      return;
    }

    const data = await response.json();
    console.log("Evento creado:", data);

    // Redirigir a la ruta de administración
    navigate("/admin/eventos");
  } catch (error) {
    console.error("Error en la solicitud:", error);
  }
  };

  const handleArchivoChange = (e) => {
    if (e.target.type === "file") {
      setArchivoTemp((prev) => ({ ...prev, file: e.target.files[0] }));
    } else {
      setArchivoTemp((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    }
  };

  const agregarArchivo = async () => {
    if (archivoTemp.file) {
      const formDataFile = new FormData();
      formDataFile.append("archivo", archivoTemp.file);

      const response = await fetch(
        "https://localhost:7294/api/Archivos/subir",
        {
          method: "POST",
          body: formDataFile,
        }
      );

      const data = await response.json();
      const nuevoArchivo = {
        orden: formData.archivos.length,
        url: data.url,
        tipo: archivoTemp.tipo,
      };

      setFormData((prev) => ({
        ...prev,
        archivos: [...prev.archivos, nuevoArchivo],
      }));

      setArchivoTemp({ url: "", tipo: "", file: null, editIndex: null });
    }
  };

  return (
    <Container fluid="sm" className="mt-3">
      <h1>Crear Evento</h1>
      <Form onSubmit={handleSubmit}>
        {/* Campos básicos */}
        <Form.Group className="mb-3">
          <Form.Label>Nombre del Evento</Form.Label>
          <Form.Control
            type="text"
            name="nombre_Evento"
            value={formData.nombre_Evento}
            onChange={handleChange}
            required
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Tipo de Evento</Form.Label>
          <Form.Select
            name="tipo"
            value={formData.tipo}
            onChange={handleChange}
            required
          >
            <option value="">Seleccione un tipo</option>
            <option value="Concurso">Concurso</option>
            <option value="Feria">Feria</option>
            <option value="Torneo">Torneo</option>
            <option value="Festival">Festival</option>
          </Form.Select>
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Fecha</Form.Label>
          <Form.Control
            type="datetime-local"
            name="fecha"
            value={formData.fecha}
            onChange={handleChange}
            required
            min={minDateStr}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Lugar (opcional)</Form.Label>
          <Form.Control
            type="text"
            name="lugar"
            value={formData.lugar}
            onChange={handleChange}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Capacidad Máxima</Form.Label>
          <Form.Control
            type="number"
            name="capacidad_Max"
            value={formData.capacidad_Max}
            onChange={handleChange}
            required
            min={1}
          />
        </Form.Group>

        {/* Encuesta */}
        <Container className="shadow p-3 mb-3">
          <Form.Label>Encuesta (opcional)</Form.Label>
          {formData.encuesta.map((p, index) => (
            <InputGroup key={index} className="mb-2">
              <InputGroup.Text>
                {index + 1}. {p.tipo}
              </InputGroup.Text>
              <Form.Control value={p.pregunta} readOnly />
              <Button variant="warning" onClick={() => editarPregunta(index)}>
                Editar
              </Button>
              <Button variant="danger" onClick={() => eliminarPregunta(index)}>
                Eliminar
              </Button>
            </InputGroup>
          ))}
          <Row className="mt-2">
            <Col>
              <Form.Control
                type="text"
                placeholder="Pregunta"
                name="pregunta"
                value={preguntaTemp.pregunta}
                onChange={handlePreguntaChange}
              />
            </Col>
            <Col>
              <Form.Select
                name="tipo"
                value={preguntaTemp.tipo}
                onChange={handlePreguntaChange}
              >
                <option value="texto">Texto</option>
                <option value="rango">Rango</option>
                <option value="numero">Numero</option>
                <option value="si/no">Si / No</option>
              </Form.Select>
            </Col>
            <Col xs="auto">
              <Button type="button" onClick={agregarPregunta}>
                {preguntaTemp.editIndex !== null ? "Actualizar" : "Agregar"}
              </Button>
            </Col>
          </Row>
        </Container>

        {/* Content */}
        <Container className="shadow p-3 mb-3">
          <Form.Label>Content (opcional)</Form.Label>
          {formData.content.map((c, index) => (
            <div key={index}>
              <Form.Control value={c.title} readOnly placeholder="Title" />
              <Form.Control
                as="textarea"
                value={c.content}
                readOnly
                placeholder="Content"
                rows={4}
              />
              <ButtonGroup>
                <Button variant="warning" onClick={() => editarContent(index)}>
                  Editar
                </Button>
                <Button variant="danger" onClick={() => eliminarContent(index)}>
                  Eliminar
                </Button>
              </ButtonGroup>
            </div>
          ))}
          <Form.Control
            type="text"
            placeholder="Title"
            name="title"
            value={contentTemp.title}
            onChange={handleContentChange}
            className="my-3"
          />
          <Form.Control
            rows={6}
            as="textarea"
            placeholder="Content"
            name="content"
            value={contentTemp.content}
            onChange={handleContentChange}
            className="mb-3"
          />
          <Button type="button" onClick={agregarContent}>
            {contentTemp.editIndex !== null ? "Actualizar" : "Agregar"}
          </Button>
        </Container>

        {/* Archivos */}
        <Container className="shadow p-3 mb-3">
          <Form.Label>Archivos (opcional)</Form.Label>
          {formData.archivos.map((a, index) => (
            <div className="position-relative m-3">
              <Image src={a.url} className="w-100 rounded-3" />
              <ButtonGroup className="position-absolute start-0 top-0">
                <Button variant="danger" onClick={() => eliminarArchivo(index)}>
                  Eliminar
                </Button>
              </ButtonGroup>
            </div>
          ))}
          <Row className="mt-2">
            <Col>
              <Form.Control
                type="file"
                name="archivo"
                onChange={handleArchivoChange}
              />
            </Col>
            {/* <Col>
              <Form.Control
                type="text"
                placeholder="Tipo"
                name="tipo"
                value={archivoTemp.tipo}
                onChange={handleArchivoChange}
              />
            </Col> */}
            <Col xs="auto">
              <Button type="button" onClick={agregarArchivo}>
                {archivoTemp.editIndex !== null ? "Actualizar" : "Agregar"}
              </Button>
            </Col>
          </Row>
        </Container>

        <Form.Group className="mb-3">
          <Form.Label>Costo (opcional)</Form.Label>
          <Form.Control
            type="number"
            name="costo"
            value={formData.costo}
            onChange={handleChange}
            min={0}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Estado</Form.Label>
          <Form.Control type="text" value={formData.estado} disabled />
        </Form.Group>

        <Button type="submit">Crear evento</Button>
      </Form>
    </Container>
  );
}
