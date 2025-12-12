import api from './api'

export const getServicios = () => api.get('/servicio')
export const getServicio = (id) => api.get(`/servicio/${id}`)
export const createServicio = (data) => api.post('/servicio', data)
export const updateServicio = (id, data) => api.put(`/servicio/${id}`, data)
export const deleteServicio = (id) => api.delete(`/servicio/${id}`)
