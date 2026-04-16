"use client"
import { useState, useEffect, useMemo } from 'react'
import { useProjects } from '../hooks/useProjects'
import { useExperiments } from '../hooks/useExperiments'
import { useTasks } from '../hooks/useTasks'
import { authApi, CreateProjectDto, CreateExperimentDto, CreateTaskDto, ProjectDto, ExperimentDto, TaskDto, LookupDto, lookupsApi, PriorityLookupDto, IntLookupDto } from '../../lib/api'
import { useRouter } from 'next/navigation'

type EntityType = 'project' | 'experiment' | 'task'

type EditableItem = ProjectDto | ExperimentDto | TaskDto | null

interface ModalProps {
  isOpen: boolean
  onClose: () => void
  title: string
  onSubmit: (data: Record<string, string | number | undefined>) => void
  fields: { name: string; label: string; type: string; options?: { id: string | number; name: string | null }[] }[]
  initialData: Record<string, string | number | undefined>
  loading?: boolean
}

function Modal({ isOpen, onClose, title, onSubmit, fields, initialData, loading }: ModalProps) {
  const [formData, setFormData] = useState<Record<string, string | number | undefined>>(initialData)

  useEffect(() => {
    setFormData(initialData)
  }, [initialData])

  if (!isOpen) return null

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    onSubmit(formData)
  }

  return (
    <div className="modal" onClick={onClose}>
      <div className="modal-content" onClick={e => e.stopPropagation()}>
        <span className="close" onClick={onClose}>&times;</span>
        <h2 className="title-design">{title}</h2>
        <form onSubmit={handleSubmit} className="auth-form">
          {fields.map(field => (
            <div key={field.name}>
              <label style={{ display: 'block', marginBottom: '5px', fontFamily: 'var(--font-primary)', color: 'var(--primary-color)' }}>
                {field.label}
              </label>
              {field.type === 'textarea' ? (
                <textarea
                  className="add-name"
                  value={String(formData[field.name] || '')}
                  onChange={e => setFormData({ ...formData, [field.name]: e.target.value })}
                />
              ) : field.type === 'select' && field.options && field.options.length > 0 ? (
                <select
                  className="add-name"
                  value={String(formData[field.name] || '')}
                  onChange={e => setFormData({ ...formData, [field.name]: e.target.value })}
                >
                  <option value="">Select...</option>
                  {field.options.map(opt => (
                    <option key={opt.id} value={opt.id}>{opt.name ?? ''}</option>
                  ))}
                </select>
              ) : (
                <input
                  type={field.type}
                  className="add-name"
                  value={String(formData[field.name] || '')}
                  onChange={e => setFormData({ ...formData, [field.name]: field.type === 'number' ? Number(e.target.value) : e.target.value })}
                />
              )}
            </div>
          ))}
          <button type="submit" className="add-button-module" disabled={loading}>
            {loading ? 'Saving...' : 'Save'}
          </button>
        </form>
      </div>
    </div>
  )
}

interface CardProps {
  title: string
  subtitle?: string
  onEdit: () => void
  onDelete: () => void
}

function Card({ title, subtitle, onEdit, onDelete }: CardProps) {
  return (
    <div className="grouped-card">
      <div className="card-header">{title}</div>
      {subtitle && <p className="description">{subtitle}</p>}
      <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
        <button className="edit-btn" onClick={onEdit}>Edit</button>
        <button className="edit-btn" style={{ backgroundColor: '#d9534f' }} onClick={onDelete}>Delete</button>
      </div>
    </div>
  )
}

export default function Home() {
  const router = useRouter()
  const { projects, loading: projectsLoading, createProject, updateProject, deleteProject } = useProjects()
  const { experiments, loading: experimentsLoading, createExperiment, updateExperiment, deleteExperiment } = useExperiments()
  const { tasks, loading: tasksLoading, createTask, updateTask, deleteTask } = useTasks()

  const [activeModal, setActiveModal] = useState<EntityType | null>(null)
  const [editingItem, setEditingItem] = useState<EditableItem>(null)
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [projectTypes, setProjectTypes] = useState<LookupDto[]>([])
  const [experimentTypes, setExperimentTypes] = useState<LookupDto[]>([])
  const [taskTypes, setTaskTypes] = useState<LookupDto[]>([])
  const [priorities, setPriorities] = useState<PriorityLookupDto[]>([])
  const [taskStatuses, setTaskStatuses] = useState<IntLookupDto[]>([])
  const [search, setSearch] = useState('')

  const projectFields = useMemo(() => [
    { name: 'projectName', label: 'Project Name', type: 'text' },
    { name: 'funding', label: 'Funding', type: 'number' },
    { name: 'requirements', label: 'Requirements', type: 'textarea' },
    { name: 'projectTypeId', label: 'Project Type', type: 'select', options: projectTypes },
  ], [projectTypes])

  const experimentFields = useMemo(() => [
    { name: 'experimentName', label: 'Experiment Name', type: 'text' },
    { name: 'experimentNotes', label: 'Notes', type: 'textarea' },
    { name: 'experimentTypeId', label: 'Experiment Type', type: 'select', options: experimentTypes },
    { name: 'projectId', label: 'Project', type: 'select', options: (projects || []).map(p => ({ id: p.id, name: p.projectName || '' })) },
  ], [projects, experimentTypes])

  const taskFields = useMemo(() => [
    { name: 'taskName', label: 'Task Name', type: 'text' },
    { name: 'taskDescription', label: 'Description', type: 'textarea' },
    { name: 'priority', label: 'Priority', type: 'select', options: priorities },
    { name: 'taskTypeId', label: 'Task Type', type: 'select', options: taskTypes },
    { name: 'experimentId', label: 'Experiment', type: 'select', options: (experiments || []).map(e => ({ id: e.id, name: e.experimentName || '' })) },
  ], [experiments, priorities, taskTypes])

  useEffect(() => {
    lookupsApi.getProjectTypes().then(setProjectTypes).catch(() => {})
    lookupsApi.getExperimentTypes().then(setExperimentTypes).catch(() => {})
    lookupsApi.getTaskTypes().then(setTaskTypes).catch(() => {})
    lookupsApi.getPriorities().then(setPriorities).catch(() => {})
    lookupsApi.getTaskStatuses().then(setTaskStatuses).catch(() => {})
  }, [])

  const handleLogout = async () => {
    const refreshToken = localStorage.getItem('refreshToken')
    if (refreshToken) {
      try {
        await authApi.logout(refreshToken)
      } catch {
        // ignore
      }
    }
    localStorage.removeItem('jwt')
    localStorage.removeItem('refreshToken')
    router.push('/login')
  }

  const openCreateModal = (type: EntityType) => {
    setEditingItem(null)
    setActiveModal(type)
  }

  const openEditModal = (type: EntityType, item: EditableItem) => {
    setEditingItem(item)
    setActiveModal(type)
  }

  const closeModal = () => {
    setActiveModal(null)
    setEditingItem(null)
    setError(null)
  }

  // Memoize initial data to prevent Modal from resetting on every parent render
  const projectInitialData = useMemo(() => {
    if (activeModal !== 'project' || !editingItem) return {}
    const p = editingItem as ProjectDto
    return {
      projectName: p.projectName ?? '',
      funding: p.funding ?? 0,
      requirements: p.requirements ?? '',
      projectTypeId: p.projectTypeId,
    }
  }, [activeModal, editingItem])

  const experimentInitialData = useMemo(() => {
    if (activeModal !== 'experiment' || !editingItem) return {}
    const e = editingItem as ExperimentDto
    return {
      experimentName: e.experimentName ?? '',
      experimentNotes: e.experimentNotes ?? '',
      experimentTypeId: e.experimentTypeId,
      projectId: e.projectId,
    }
  }, [activeModal, editingItem])

  const taskInitialData = useMemo(() => {
    if (activeModal !== 'task' || !editingItem) return {}
    const t = editingItem as TaskDto
    return {
      taskName: t.taskName ?? '',
      taskDescription: t.taskDescription ?? '',
      priority: t.priority ?? 0,
      taskTypeId: t.taskTypeId,
      experimentId: t.experimentId,
    }
  }, [activeModal, editingItem])

  const handleSubmit = async (data: Record<string, string | number | undefined>) => {
    setSubmitting(true)
    setError(null)
    try {
      if (activeModal === 'project') {
        const projectData: CreateProjectDto = {
          projectName: (data.projectName ?? '') as string | null,
          funding: (data.funding ?? 0) as number | null,
          requirements: (data.requirements ?? '') as string | null,
          requirementsFilePath: null,
          projectTypeId: (data.projectTypeId ?? '') as string,
        }
        if (editingItem) {
          await updateProject((editingItem as ProjectDto).id, projectData)
        } else {
          await createProject(projectData)
        }
      } else if (activeModal === 'experiment') {
        const experimentData: CreateExperimentDto = {
          experimentName: (data.experimentName ?? '') as string | null,
          experimentNotes: (data.experimentNotes ?? '') as string | null,
          experimentTypeId: (data.experimentTypeId ?? '') as string,
          projectId: (data.projectId ?? '') as string,
        }
        if (editingItem) {
          await updateExperiment((editingItem as ExperimentDto).id, experimentData)
        } else {
          await createExperiment(experimentData)
        }
      } else if (activeModal === 'task') {
        const taskData: CreateTaskDto = {
          taskName: (data.taskName ?? '') as string | null,
          taskDescription: (data.taskDescription ?? '') as string | null,
          priority: data.priority ? Number(data.priority) : null,
          taskTypeId: (data.taskTypeId ?? '') as string,
          experimentId: (data.experimentId ?? '') as string,
          assignedUserId: (data.assignedUserId ?? null) as string | null,
        }
        if (editingItem) {
          await updateTask((editingItem as TaskDto).id, taskData)
        } else {
          await createTask(taskData)
        }
      }
      closeModal()
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : 'An error occurred')
    } finally {
      setSubmitting(false)
    }
  }

  const handleDelete = async (type: EntityType, id: string) => {
    if (!confirm('Are you sure you want to delete this item?')) return
    try {
      if (type === 'project') await deleteProject(id)
      else if (type === 'experiment') await deleteExperiment(id)
      else if (type === 'task') await deleteTask(id)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : 'An error occurred')
    }
  }

  const loading = projectsLoading || experimentsLoading || tasksLoading

  const searchLower = search.toLowerCase()
  const filteredProjects = projects.filter(p => p.projectName?.toLowerCase().includes(searchLower))
  const filteredExperiments = experiments.filter(e => e.experimentName?.toLowerCase().includes(searchLower))
  const filteredTasks = tasks.filter(t => t.taskName?.toLowerCase().includes(searchLower))

  return (
    <main>
      <div className="header-bar display">
        <div className="web-name">Lab manager ꨄ︎</div>
        <input className="search-box" placeholder="Search..." value={search} onChange={e => setSearch(e.target.value)} />
        <button className="search-button" onClick={handleLogout}>Logout</button>
      </div>

      {error && (
        <div style={{ padding: '10px 30px', backgroundColor: '#f8d7da', color: '#721c24', margin: '10px 30px', borderRadius: '5px' }}>
          {error}
        </div>
      )}

      <div id="flashy-content">
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <div className="title title-design">Projects</div>
          <button className="add-button" onClick={() => openCreateModal('project')}>+ Add Project</button>
        </div>
        <div className="card-body task-section">
          {filteredProjects.length === 0 ? (
            <p style={{ color: '#999' }}>{search ? 'No matching projects' : 'No projects yet'}</p>
          ) : (
            filteredProjects.map(project => (
              <Card
                key={project.id}
                title={project.projectName || 'Untitled'}
                subtitle={project.requirements || undefined}
                onEdit={() => openEditModal('project', project)}
                onDelete={() => handleDelete('project', project.id)}
              />
            ))
          )}
        </div>

        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <div className="title title-design">Experiments</div>
          <button className="add-button" onClick={() => openCreateModal('experiment')}>+ Add Experiment</button>
        </div>
        <div className="card-body due-section">
          {filteredExperiments.length === 0 ? (
            <p style={{ color: '#999' }}>{search ? 'No matching experiments' : 'No experiments yet'}</p>
          ) : (
            filteredExperiments.map(experiment => (
              <Card
                key={experiment.id}
                title={experiment.experimentName || 'Untitled'}
                subtitle={experiment.experimentNotes || undefined}
                onEdit={() => openEditModal('experiment', experiment)}
                onDelete={() => handleDelete('experiment', experiment.id)}
              />
            ))
          )}
        </div>

        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <div className="title title-design">Tasks</div>
          <button className="add-button" onClick={() => openCreateModal('task')}>+ Add Task</button>
        </div>
        <div className="card-body upcoming-section">
          {filteredTasks.length === 0 ? (
            <p style={{ color: '#999' }}>{search ? 'No matching tasks' : 'No tasks yet'}</p>
          ) : (
            filteredTasks.map(task => (
              <Card
                key={task.id}
                title={task.taskName || 'Untitled'}
                subtitle={`Status: ${taskStatuses.find(s => s.id === task.status)?.name ?? task.status} - Type: ${taskTypes.find(t => t.id === task.taskTypeId)?.name || task.taskTypeName || 'Unknown'} - Priority: ${priorities.find(p => p.id === task.priority)?.name ?? task.priority ?? 'N/A'}`}
                onEdit={() => openEditModal('task', task)}
                onDelete={() => handleDelete('task', task.id)}
              />
            ))
          )}
        </div>
      </div>

      <Modal
        isOpen={activeModal === 'project'}
        onClose={closeModal}
        title={editingItem ? 'Edit Project' : 'New Project'}
        onSubmit={handleSubmit}
        fields={projectFields}
        initialData={projectInitialData}
        loading={submitting}
      />

      <Modal
        isOpen={activeModal === 'experiment'}
        onClose={closeModal}
        title={editingItem ? 'Edit Experiment' : 'New Experiment'}
        onSubmit={handleSubmit}
        fields={experimentFields}
        initialData={experimentInitialData}
        loading={submitting}
      />

      <Modal
        isOpen={activeModal === 'task'}
        onClose={closeModal}
        title={editingItem ? 'Edit Task' : 'New Task'}
        onSubmit={handleSubmit}
        fields={taskFields}
        initialData={taskInitialData}
        loading={submitting}
      />
    </main>
  )
}
