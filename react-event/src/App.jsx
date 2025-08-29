import "./App.css";
import NavbarOffcanvas from "./components/NavbarOffcanvas";
import { UserProvider } from "./contexts/UserContext";
import MisRutas from "./routes";

function App() {
  return (
    <UserProvider>
      <MisRutas />
    </UserProvider>
  );
}

export default App;
