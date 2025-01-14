export interface RespuestaAPI<T> {
    Data: T | null;
    Code: number | null;
    Mensaje: string | null;
}