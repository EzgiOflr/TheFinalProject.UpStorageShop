import React from 'react'
import {
    CAvatar,
    CBadge,
    CDropdown,
    CDropdownDivider,
    CDropdownHeader,
    CDropdownItem,
    CDropdownMenu,
    CDropdownToggle,
} from '@coreui/react'
import {
    cilBell,
    cilCreditCard,
    cilCommentSquare,
    cilEnvelopeOpen,
    cilFile,
    cilLockLocked,
    cilSettings,
    cilTask,
    cilUser,
} from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { useDispatch, useSelector } from 'react-redux'
import { RootState } from '../../redux/store'
import { clearNotificationCount } from '../../redux/reducers/authSlice'


const AppHeaderDropdown = () => {
    const dispatch = useDispatch();
    const auth = useSelector((state: RootState) => state.auth)

    return (
        <CDropdown variant="nav-item">
            <CDropdownToggle className="py-0" caret={false}>
                <CIcon onClick={() => dispatch(clearNotificationCount())} icon={cilBell} size="lg" />
                <CBadge color="danger" className="ms-1">
                    {auth.notificationList.filter(x => !x.isSeen).length}
                </CBadge>
            </CDropdownToggle>
            <CDropdownMenu style={{ width: '500px', }} className="pt-0">
                <CDropdownHeader className="bg-light fw-semibold py-2">Notifications</CDropdownHeader>

                <div style={{ maxHeight: '300px', overflowY: 'scroll' }}>
                    {
                        auth.notificationList.map((item, index) =>
                            <div className='p-3' key={'noti' + index}>
                                {item.message}
                            </div>
                        )
                    }
                </div>
            </CDropdownMenu>
        </CDropdown>
    )
}

export default AppHeaderDropdown
