import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { login } from '../services/authService'

export default function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState(null)
  const nav = useNavigate()

  const handle = async (e) => {
    e.preventDefault()
    try {
      const data = await login(email, password)
      localStorage.setItem('token', data.token || data)
      nav('/')
    } catch (err) {
      setError('Credenciales inválidas')
    }
  }

  return (
    <div className="container">
      <h2>Login</h2>
      <form onSubmit={handle}>
        <div>
          <label>Email</label>
          <input value={email} onChange={e => setEmail(e.target.value)} />
        </div>
        <div>
          <label>Password</label>
          <input type="password" value={password} onChange={e => setPassword(e.target.value)} />
        </div>
        <button type="submit">Ingresar</button>
      </form>
      {error && <p style={{color:'red'}}>{error}</p>}
    </div>
  )
}
