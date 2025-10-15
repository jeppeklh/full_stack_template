import { Outlet, Link } from "react-router-dom";

export default function App() {
  return (
    <>
      <nav style={{ padding: 12, borderBottom: "1px solid #eee" }}>
        <Link to="/">Home</Link>
      </nav>
      <main style={{ padding: 16 }}>
        <Outlet />
      </main>
    </>
  );
}
