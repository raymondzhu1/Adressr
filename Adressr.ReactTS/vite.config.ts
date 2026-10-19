import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'

export default defineConfig({
  plugins: [react()],
  server: {
    port: 4200,
    strictPort: true
    // proxy: {
    //   "/api": {
    //     target: "https://localhost:52152",
    //     secure: false
    //   }
    // }
  }
});
