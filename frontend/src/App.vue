<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const API_URL = 'http://localhost:8080/api'
const API_KEY = 'SecretApiKey123'

// Configuración global de Axios para incluir el API Key
axios.defaults.headers.common['X-Api-Key'] = API_KEY

const movies = ref([])
const categories = ref([])
const loading = ref(false)

const newMovie = ref({
  titulo: '',
  descripcion: '',
  precio: 0,
  categoryId: '',
  estado: 'Activa'
})

const newCategory = ref({
  nombre: '',
  descripcion: ''
})

const fetchCategories = async () => {
  try {
    const response = await axios.get(`${API_URL}/categories`)
    categories.value = response.data
  } catch (error) {
    console.error('Error fetching categories:', error)
  }
}

const fetchMovies = async () => {
  loading.value = true
  try {
    const response = await axios.get(`${API_URL}/movies`)
    movies.value = response.data
  } catch (error) {
    console.error('Error fetching movies:', error)
  } finally {
    loading.value = false
  }
}

const addCategory = async () => {
  if (!newCategory.value.nombre) return
  try {
    await axios.post(`${API_URL}/categories`, newCategory.value)
    newCategory.value = { nombre: '', descripcion: '' }
    fetchCategories()
  } catch (error) {
    if (error.response?.data?.detail) {
      alert(`Error: ${error.response.data.detail}`)
    } else {
      console.error('Error adding category:', error)
    }
  }
}

const addMovie = async () => {
  if (!newMovie.value.titulo || !newMovie.value.categoryId) {
    alert('Título y Categoría son requeridos')
    return
  }
  try {
    await axios.post(`${API_URL}/movies`, newMovie.value)
    newMovie.value = { titulo: '', descripcion: '', precio: 0, categoryId: '', estado: 'Activa' }
    fetchMovies()
  } catch (error) {
    if (error.response?.data?.errors) {
      const errors = Object.values(error.response.data.errors).flat().join('\n')
      alert(`Validación Errónea:\n${errors}`)
    } else {
      console.error('Error adding movie:', error)
    }
  }
}

const deleteMovie = async (id) => {
  if (!confirm('¿Seguro que deseas eliminar esta película?')) return
  try {
    await axios.delete(`${API_URL}/movies/${id}`)
    fetchMovies()
  } catch (error) {
    console.error('Error deleting movie:', error)
  }
}

onMounted(() => {
  fetchCategories()
  fetchMovies()
})
</script>

<template>
  <div class="container">
    <header>
      <h1>🎬 Distribución de Películas</h1>
      <p class="subtitle">Con conversión automática de USD a COP</p>
    </header>

    <div class="grid">
      <!-- Sección de Categorías -->
      <section class="card">
        <h2>Nueva Categoría</h2>
        <div class="form-group">
          <input v-model="newCategory.nombre" placeholder="Nombre de Categoría (Requerido)" />
          <textarea v-model="newCategory.descripcion" placeholder="Descripción (Opcional)"></textarea>
          <button @click="addCategory" class="btn-primary">Agregar Categoría</button>
        </div>

        <div class="list-minimal">
          <h3>Categorías Existentes</h3>
          <ul>
            <li v-for="cat in categories" :key="cat.id">{{ cat.nombre }}</li>
          </ul>
        </div>
      </section>

      <!-- Sección de Películas -->
      <section class="card">
        <h2>Nueva Película</h2>
        <div class="form-group">
          <input v-model="newMovie.titulo" placeholder="Título (Requerido)" />
          <textarea v-model="newMovie.descripcion" placeholder="Descripción (Opcional)"></textarea>
          <input v-model.number="newMovie.precio" type="number" step="0.01" placeholder="Precio (USD)" />

          <select v-model="newMovie.categoryId">
            <option value="" disabled>Selecciona una Categoría</option>
            <option v-for="cat in categories" :key="cat.id" :value="cat.id">
              {{ cat.nombre }}
            </option>
          </select>

          <select v-model="newMovie.estado">
            <option value="Activa">Activa</option>
            <option value="Inactiva">Inactiva</option>
          </select>

          <button @click="addMovie" class="btn-success">Guardar Película</button>
        </div>
      </section>
    </div>

    <hr />

    <!-- Listado de Películas -->
    <section class="movies-list">
      <h2>Listado de Películas</h2>
      <div v-if="loading">Cargando...</div>
      <table v-else>
        <thead>
          <tr>
            <th>Título</th>
            <th>Categoría</th>
            <th>Precio (USD)</th>
            <th>Precio (COP)</th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="movie in movies" :key="movie.id">
            <td>
              <strong>{{ movie.titulo }}</strong>
              <p class="small">{{ movie.descripcion }}</p>
            </td>
            <td>{{ movie.categoryNombre }}</td>
            <td>${{ movie.precioUsd.toFixed(2) }}</td>
            <td class="cop-price">COP {{ movie.precioCop.toLocaleString() }}</td>
            <td>
              <span :class="['badge', movie.estado.toLowerCase()]">{{ movie.estado }}</span>
            </td>
            <td>
              <button @click="deleteMovie(movie.id)" class="btn-danger">Eliminar</button>
            </td>
          </tr>
        </tbody>
      </table>
    </section>
  </div>
</template>

<style>
:root {
  --primary: #2563eb;
  --success: #16a34a;
  --danger: #dc2626;
  --bg: #f8fafc;
  --text: #1e293b;
}

body {
  background-color: var(--bg);
  color: var(--text);
  font-family: 'Inter', system-ui, sans-serif;
  margin: 0;
}

.container {
  max-width: 1100px;
  margin: 0 auto;
  padding: 20px;
}

header {
  text-align: center;
  margin-bottom: 40px;
}

.subtitle {
  color: #64748b;
  margin-top: -10px;
}

.grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
  margin-bottom: 40px;
}

.card {
  background: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1);
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

input,
textarea,
select {
  padding: 10px;
  border: 1px solid #e2e8f0;
  border-radius: 6px;
  font-size: 14px;
}

button {
  padding: 10px;
  border: none;
  border-radius: 6px;
  font-weight: 600;
  cursor: pointer;
  transition: opacity 0.2s;
}

button:hover {
  opacity: 0.9;
}

.btn-primary {
  background: var(--primary);
  color: white;
}

.btn-success {
  background: var(--success);
  color: white;
}

.btn-danger {
  background: var(--danger);
  color: white;
  padding: 5px 10px;
  font-size: 12px;
}

table {
  width: 100%;
  border-collapse: collapse;
  background: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1);
}

th,
td {
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #f1f5f9;
}

th {
  background: #f1f5f9;
  font-weight: 600;
}

.cop-price {
  font-weight: 600;
  color: var(--success);
}

.small {
  font-size: 12px;
  color: #64748b;
  margin: 0;
}

.badge {
  padding: 4px 8px;
  border-radius: 9999px;
  font-size: 12px;
  font-weight: 600;
}

.badge.activa {
  background: #dcfce7;
  color: #166534;
}

.badge.inactiva {
  background: #fee2e2;
  color: #991b1b;
}

.list-minimal {
  margin-top: 20px;
  border-top: 1px solid #f1f5f9;
}

.list-minimal ul {
  list-style: none;
  padding: 0;
  max-height: 150px;
  overflow-y: auto;
}

.list-minimal li {
  font-size: 14px;
  padding: 5px 0;
}
</style>
