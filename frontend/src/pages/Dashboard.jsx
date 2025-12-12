import React from 'react'
import { Link, useNavigate } from 'react-router-dom'

export default function Dashboard(){
  const nav = useNavigate()
  const logout = () => { localStorage.removeItem('token'); nav('/login') }
  return (
    <div className="container">
      <div className="header">
        <h2>Dashboard</h2>
        <div>
          <button onClick={logout}>Logout</button>
        </div>
      </div>
      <div className="nav">
        <Link to="/clientes">Clientes</Link>
        <Link to="/servicios">Servicios</Link>
        <Link to="/reservas">Reservas</Link>
      </div>
    </div>
  )
}
