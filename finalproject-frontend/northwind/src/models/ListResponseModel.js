import ResponseModel from '../models/ResponseModel';

export default class ListResponseModel extends ResponseModel {
    constructor(success, message, data = []) {
        super(success, message);
        this.data = data;
    }
}
