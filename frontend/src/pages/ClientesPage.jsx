import React, { useEffect, useState } from 'react'
import { getClientes, createCliente, updateCliente, deleteCliente } from '../services/clientesService'
import { useNavigate } from 'react-router-dom'

export default function ClientesPage(){
  const [list, setList] = useState([])
  const [form, setForm] = useState({ nombre:'', email:'', telefono:'' })
  const [editing, setEditing] = useState(null)
  const nav = useNavigate()

  const load = async () => {
    const res = await getClientes()
    setList(res.data)
  }

  useEffect(()=>{ load() }, [])

  const submit = async (e) =>{
    e.preventDefault()
    if(editing){
      await updateCliente(editing, form)
      setEditing(null)
    } else {
      await createCliente(form)
    }
    setForm({ nombre:'', email:'', telefono:'' })
    load()
  }

  const edit = (c) => { setEditing(c.clienteId); setForm({ nombre:c.nombre, email:c.email, telefono:c.telefono }) }
  const remove = async (id) => { if(confirm('Eliminar?')){ await deleteCliente(id); load() } }

  return (
    <div className="container">
      <div className="header">
        <h2>Clientes</h2>
        <div>
          <button onClick={()=>nav('/')}>Volver al Dashboard</button>
        </div>
      </div>

      <form onSubmit={submit}>
        <input placeholder="Nombre" value={form.nombre} onChange={e=>setForm({...form,nombre:e.target.value})} />
        <input placeholder="Email" value={form.email} onChange={e=>setForm({...form,email:e.target.value})} />
        <input placeholder="Telefono" value={form.telefono} onChange={e=>setForm({...form,telefono:e.target.value})} />
        <button type="submit">{editing? 'Actualizar' : 'Crear'}</button>
      </form>

      <table className="table">
        <thead><tr><th>Id</th><th>Nombre</th><th>Email</th><th>Tel</th><th>Acciones</th></tr></thead>
        <tbody>
          {list.map(c => (
            <tr key={c.clienteId}>
              <td>{c.clienteId}</td>
              <td>{c.nombre}</td>
              <td>{c.email}</td>
              <td>{c.telefono}</td>
              <td>
                <button onClick={()=>edit(c)}>Editar</button>
                <button onClick={()=>remove(c.clienteId)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
