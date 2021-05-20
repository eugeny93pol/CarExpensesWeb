export class HttpError extends Error {
  constructor(public code: number, message: string, public time: number) {
    super(message)
    Object.setPrototypeOf(this, HttpError.prototype)
  }
}