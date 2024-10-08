import ResponseModel from './ResponseModel';

export default class SingleResponseModel extends ResponseModel {
    constructor(success, message, data = null) {
        super(success, message);
        this.data = data;
    }
}
