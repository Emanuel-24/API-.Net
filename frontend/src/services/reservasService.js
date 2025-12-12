import api from './api'

export const getReservas = () => api.get('/reserva/active')
export const getAllReservas = () => api.get('/reserva/completas')
export const createReserva = (data) => api.post('/reserva', data)
export const updateReserva = (id, data) => api.put(`/reserva/${id}`, data)
export const getReservasByCliente = (clienteId) => api.get(`/reserva/cliente/${clienteId}`)
export const getReservaFull = (id) => api.get(`/reserva/${id}/detalle`)
