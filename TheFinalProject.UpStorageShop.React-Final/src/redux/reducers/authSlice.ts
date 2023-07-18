import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface authSlice {
    user: UserObject | undefined,
}

interface UserObject {
    id: string,
    email: string,
    firstName: string,
    lastName: string,
    expires: string | null,
    accessToken: string | null,
}

const initialState: authSlice = {
    user: undefined,
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        storeUser: (state, action: PayloadAction<UserObject>) => {
            state.user = action.payload
        },
    },
})

// Action creators are generated for each case reducer function
export const { storeUser } = authSlice.actions

export default authSlice.reducer;