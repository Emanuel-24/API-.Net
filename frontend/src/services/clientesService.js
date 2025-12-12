import api from './api'

export const getClientes = () => api.get('/cliente')
export const getCliente = (id) => api.get(`/cliente/${id}`)
export const createCliente = (data) => api.post('/cliente', data)
export const updateCliente = (id, data) => api.put(`/cliente/${id}`, data)
export const deleteCliente = (id) => api.delete(`/cliente/${id}`)
