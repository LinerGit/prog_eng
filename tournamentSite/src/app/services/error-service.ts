import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})
export class ErrorService {
   constructor() {}

  handleError(error: unknown, customMessage?: string): void {
    let summary = 'Ошибка';
    let detail = customMessage || 'Произошла неизвестная ошибка.';

    if (error instanceof HttpErrorResponse) {
      if (error.status === 0) {
        detail = customMessage || 'Нет соединения с сервером.';
      } else if (error.status === 400) {
        detail = customMessage || (error.error?.message || 'Некорректный запрос.');
      } else if (error.status === 401) {
        detail = customMessage || 'Необходима авторизация.';
      } else if (error.status === 403) {
        detail = customMessage || 'Доступ запрещён.';
      } else if (error.status === 404) {
        detail = customMessage || 'Запрашиваемый ресурс не найден.';
      } else if (error.status >= 500) {
        detail = customMessage || 'Внутренняя ошибка сервера.';
      } else {
        detail = customMessage || (error.error?.message || `Ошибка ${error.status}: ${error.statusText}`);
      }
    } else if (error instanceof Error) {
      detail = customMessage || error.message;
    }

    console.error(`${summary}: ${detail}`);
  }
}
