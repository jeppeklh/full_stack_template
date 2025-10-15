// app/routes/home.tsx
import { Welcome } from "../welcome/welcome";

export default function Home() {
  document.title = "Home";
  return <Welcome />;
}
