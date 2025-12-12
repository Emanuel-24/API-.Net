import React from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import Dashboard from './pages/Dashboard'
import ClientesPage from './pages/ClientesPage'
import ServiciosPage from './pages/ServiciosPage'
import ReservasPage from './pages/ReservasPage'

const Private = ({ children }) => {
  const token = localStorage.getItem('token')
  return token ? children : <Navigate to="/login" />
}

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/" element={<Private><Dashboard /></Private>} />
      <Route path="/clientes" element={<Private><ClientesPage /></Private>} />
      <Route path="/servicios" element={<Private><ServiciosPage /></Private>} />
      <Route path="/reservas" element={<Private><ReservasPage /></Private>} />
    </Routes>
  )
}
