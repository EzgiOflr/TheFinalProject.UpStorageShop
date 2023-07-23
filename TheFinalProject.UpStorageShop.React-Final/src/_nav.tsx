import React from 'react'
import CIcon from '@coreui/icons-react'
import {
    cilAccountLogout,
    cilBell,
    cilCalculator,
    cilChartPie,
    cilCog,
    cilCursor,
    cilDescription,
    cilDrop,
    cilList,
    cilNotes,
    cilPencil,
    cilPlus,
    cilPuzzle,
    cilSpeedometer,
    cilStar,
} from '@coreui/icons'
import { CNavGroup, CNavItem, CNavTitle } from '@coreui/react'

const _nav = [
    {
        component: CNavItem,
        name: 'Create Order',
        to: '/',
        icon: <CIcon icon={cilPlus} customClassName="nav-icon" />,
    },
    {
        component: CNavItem,
        name: 'Orders',
        to: '/orders',
        icon: <CIcon icon={cilList} customClassName="nav-icon" />,
    },
    {
        component: CNavItem,
        name: 'Live Log',
        to: '/live-log',
        icon: <CIcon icon={cilList} customClassName="nav-icon" />,
    },
    {
        component: CNavItem,
        name: 'Users',
        to: '/users',
        icon: <CIcon icon={cilList} customClassName="nav-icon" />,
    },
    {
        component: CNavItem,
        name: 'Settings',
        to: '/settings',
        icon: <CIcon icon={cilCog} customClassName="nav-icon" />,
    },
    {
        component: CNavItem,
        name: 'Logout',
        to: '/logout',
        icon: <CIcon icon={cilAccountLogout} customClassName="nav-icon" />,
    },
]


export default _nav