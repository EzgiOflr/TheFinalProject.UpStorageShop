import React from 'react'
import CreateOrder from './views/pages/create-order/CreateOrder'
import Orders from './views/pages/orders/Orders'
import LiveLog from './views/pages/live-log/LiveLog'
import Users from './views/pages/users/Users'
import Settings from './views/pages/settings/Settings'
import Logout from './views/pages/logout/Logout'


const routes = [
  { path: '/', exact: true, name: 'Home', element: CreateOrder },
  { path: '/orders', name: 'Orders', element: Orders },
  { path: '/live-log', name: 'LiveLog', element: LiveLog },
  { path: '/users', name: 'Users', element: Users },
  { path: '/settings', name: 'Settings', element: Settings },
  { path: '/logout', name: 'Logout', element: Logout },
]

export default routes