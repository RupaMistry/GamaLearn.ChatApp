import { Outlet, Navigate } from 'react-router-dom'
import { useAuth } from './AuthContext'

export const PrivateRoutes = () => {
    const { validateUserSession } = useAuth()
    return (
        <>
            {validateUserSession() ? <Outlet /> : <Navigate to="/login" />}
        </>
    )
}