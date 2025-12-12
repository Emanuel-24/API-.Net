import React, { useEffect, useState } from 'react'
import { getAllReservas, createReserva, updateReserva, getReservasByCliente } from '../services/reservasService'
import { getClientes } from '../services/clientesService'
import { getServicios } from '../services/serviciosService'
import { useNavigate } from 'react-router-dom'

export default function ReservasPage(){
  const [list, setList] = useState([])
  const [clientes, setClientes] = useState([])
  const [servicios, setServicios] = useState([])
  const [form, setForm] = useState({ clienteId:'', servicioIds:[], fechaReserva:'', observaciones:'' })
  const nav = useNavigate()

  const load = async () => { const res = await getAllReservas(); setList(res.data) }

  useEffect(()=>{
    (async ()=>{
      load()
      const c = await getClientes(); setClientes(c.data)
      const s = await getServicios(); setServicios(s.data)
    })()
  }, [])

  const submit = async (e) =>{
    e.preventDefault()
    const payload = { ...form, clienteId: parseInt(form.clienteId), servicioIds: form.servicioIds.map(x=>parseInt(x)), fechaReserva: form.fechaReserva }
    await createReserva(payload)
    setForm({ clienteId:'', servicioIds:[], fechaReserva:'', observaciones:'' })
    load()
  }

  const toggleServicio = (id) =>{
    const exists = form.servicioIds.includes(id)
    setForm({...form, servicioIds: exists ? form.servicioIds.filter(x=>x!==id) : [...form.servicioIds, id] })
  }

  const changeEstado = async (id, estado) => { await updateReserva(id, { estado }); load() }
  const filterByCliente = async (clienteId) => {
    if(!clienteId) {
      load();
    } else {
      const clienteIdParsed = parseInt(clienteId)
      const res = await getReservasByCliente(clienteIdParsed)
     
      const mapped = res.data.map(r => {
        if (!r.cliente) {
          const found = clientes.find(c => c.clienteId === clienteIdParsed)
          r.cliente = found ?? { clienteId: clienteIdParsed, nombre: '' }
        }
        return r
      })
      setList(mapped)
    }
  }

  return (
    <div className="container">
      <div className="header">
        <h2>Reservas</h2>
        <div>
          <button onClick={()=>nav('/')}>Volver al Dashboard</button>
        </div>
      </div>

      <form onSubmit={submit}>
        <select value={form.clienteId} onChange={e=>setForm({...form, clienteId:e.target.value})}>
          <option value="">Seleccione cliente</option>
          {clientes.map(c=> <option key={c.clienteId} value={c.clienteId}>{c.nombre}</option>)}
        </select>

        <div>
          <label>Servicios</label>
          {servicios.map(s=> (
            <div key={s.servicioId}>
              <label>
                <input type="checkbox" checked={form.servicioIds.includes(s.servicioId)} onChange={()=>toggleServicio(s.servicioId)} /> {s.nombre}
              </label>
            </div>
          ))}
        </div>

        <input type="datetime-local" value={form.fechaReserva} onChange={e=>setForm({...form,fechaReserva:e.target.value})} />
        <input placeholder="Observaciones" value={form.observaciones} onChange={e=>setForm({...form,observaciones:e.target.value})} />
        <button type="submit">Crear Reserva</button>
      </form>

      <div style={{marginTop:20}}>
        <label>Filtrar por cliente</label>
        <select onChange={e=>filterByCliente(e.target.value)}>
          <option value="">Todos</option>
          {clientes.map(c=> <option key={c.clienteId} value={c.clienteId}>{c.nombre}</option>)}
        </select>
      </div>

      <table className="table">
        <thead><tr><th>Id</th><th>Cliente</th><th>Servicios</th><th>Fecha</th><th>Estado</th><th>Acciones</th></tr></thead>
        <tbody>
          {list.map(r=> (
            <tr key={r.reservaId}>
              <td>{r.reservaId}</td>
              <td>{r.cliente?.nombre}</td>
              <td>{r.serviciosReservas?.map(sr=>sr.servicio?.nombre).join(', ')}</td>
              <td>{new Date(r.fechaReserva).toLocaleString()}</td>
              <td>{r.estado}</td>
              <td>
                {r.estado !== 'Confirmada' && <button onClick={()=>changeEstado(r.reservaId,'Confirmada')}>Confirmar</button>}
                {r.estado !== 'Cancelada' && <button onClick={()=>changeEstado(r.reservaId,'Cancelada')}>Cancelar</button>}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
