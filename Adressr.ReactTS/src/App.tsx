import { RouterProvider } from "react-router";
import { AuthProvider } from "./auth/AuthContext";
import { router } from "./router";
import { AuthQueryProvider } from "./auth/AuthQueryProvider";

export default function App() {
  return (
    <AuthProvider>
      <AuthQueryProvider>
        <RouterProvider router={router} />
      </AuthQueryProvider>
    </AuthProvider>
  );
}