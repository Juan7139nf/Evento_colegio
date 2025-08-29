import "./App.css";
import NavbarOffcanvas from "./components/NavbarOffcanvas";
import MisRutas from "./routes";

function App() {
  return (
    <>
      <NavbarOffcanvas />
      <div className="">
        <MisRutas />
      </div>
    </>
  );
}

export default App;
