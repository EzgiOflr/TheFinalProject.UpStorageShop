import jwtDecode from "jwt-decode";


export function getClaimsFromJwt(token: string | null) {

    const decodedJwt = jwtDecode(token + '');

    console.log("decodedJwt", decodedJwt)

    if (!decodedJwt || typeof decodedJwt !== "object")
        return {};

    // @ts-ignore
    const { uid, email, given_name, family_name, jti, isNotificationAllowed, isMailAllowed } = decodedJwt;

    return {
        uid,
        email,
        given_name,
        family_name,
        jti,
        isNotificationAllowed,
        isMailAllowed
    }
}