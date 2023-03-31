export default class ApiClien {
  get(url: string, onSuccess: Function, onError: Function) {
    fetch(url)
      .then((response: JSON) => onSuccess(response))
      .catch((error) => onError(error));
  }
}
