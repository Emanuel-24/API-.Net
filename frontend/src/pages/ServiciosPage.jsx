import React, { useEffect, useState } from 'react'
import { getServicios, createServicio, updateServicio, deleteServicio } from '../services/serviciosService'
import { useNavigate } from 'react-router-dom'

export default function ServiciosPage(){
  const [list, setList] = useState([])
  const [form, setForm] = useState({ nombre:'', descripcion:'', precio:0 })
  const [editing, setEditing] = useState(null)
  const nav = useNavigate()

  const load = async () => { const res = await getServicios(); setList(res.data) }
  useEffect(()=>{ load() }, [])

  const submit = async (e) =>{
    e.preventDefault()
    if(editing){ await updateServicio(editing, form); setEditing(null) }
    else { await createServicio(form) }
    setForm({ nombre:'', descripcion:'', precio:0 })
    load()
  }

  const edit = (s) => { setEditing(s.servicioId); setForm({ nombre:s.nombre, descripcion:s.descripcion, precio:s.precio }) }
  const remove = async (id) => { if(confirm('Eliminar?')){ await deleteServicio(id); load() } }

  return (
    <div className="container">
      <div className="header">
        <h2>Servicios</h2>
        <div>
          <button onClick={()=>nav('/')}>Volver al Dashboard</button>
        </div>
      </div>

      <form onSubmit={submit}>
        <input placeholder="Nombre" value={form.nombre} onChange={e=>setForm({...form,nombre:e.target.value})} />
        <input placeholder="Descripcion" value={form.descripcion} onChange={e=>setForm({...form,descripcion:e.target.value})} />
        <input type="number" placeholder="Precio" value={form.precio} onChange={e=>setForm({...form,precio:parseFloat(e.target.value)})} />
        <button type="submit">{editing? 'Actualizar' : 'Crear'}</button>
      </form>

      <table className="table">
        <thead><tr><th>Id</th><th>Nombre</th><th>Precio</th><th>Acciones</th></tr></thead>
        <tbody>
          {list.map(s => (
            <tr key={s.servicioId}>
              <td>{s.servicioId}</td>
              <td>{s.nombre}</td>
              <td>{s.precio}</td>
              <td>
                <button onClick={()=>edit(s)}>Editar</button>
                <button onClick={()=>remove(s.servicioId)}>Eliminar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
