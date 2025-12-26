export interface Player {
    id: number,
    avatarPhoto: string,
    name: string,
    email: string,
    password: string,
    isCaptain: boolean,
    tournamentIds: number[]
}

