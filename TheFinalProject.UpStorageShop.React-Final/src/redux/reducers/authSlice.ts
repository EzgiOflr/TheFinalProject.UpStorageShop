import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { MessageModel } from '../../types/MessageModel'

export interface authSlice {
    user: UserObject | undefined,
    notificationList: MessageModel[]
}

interface UserObject {
    id: string
    email: string,
    firstName: string,
    lastName: string,
    expires: string | null,
    accessToken: string | null,
    isNotificationAllowed: boolean,
    isMailAllowed: boolean
}

const initialState: authSlice = {
    user: undefined,
    notificationList: []
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        storeUser: (state, action: PayloadAction<UserObject>) => {
            state.user = action.payload
        },
        storeNotification: (state, action: PayloadAction<MessageModel>) => {
            state.notificationList = state.notificationList.concat({
                ...action.payload,
                isSeen: state.user?.isNotificationAllowed ? action.payload.isSeen : true
            })
        },
        storeNewNotificationStatus: (state, action: PayloadAction<boolean>) => {
            state.user = !state.user ? undefined : {
                ...state.user,
                isNotificationAllowed: action.payload
            }
        },
        storeNewMailStatus: (state, action: PayloadAction<boolean>) => {
            state.user = !state.user ? undefined : {
                ...state.user,
                isMailAllowed: action.payload
            }
        },
        clearNotificationCount: (state) => {
            state.notificationList = state.notificationList.map(function (x) {
                x.isSeen = true;
                return x;
            })
        },
    },
})

// Action creators are generated for each case reducer function
export const { storeUser, storeNotification, clearNotificationCount, storeNewNotificationStatus, storeNewMailStatus } = authSlice.actions

export default authSlice.reducer;