import Api from "@/apis/todo.js";

export default {
  async GetList(archivedOnly, unarchivedOnly = true, noteId) {
    return Api()
      .get("/Item", { params: {archivedOnly, unarchivedOnly, noteId} })
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },

  async Add(description) {
    console.log(description)
    return Api()
      .post("/Item", description)
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },
  
  async Get(id) {
    return Api()
      .get("/Item", { params: id })
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },

  async Update(id, description) {
    return Api()
      .put("/Item/" + id, description)
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },

  async ToggleComplete(id) {
    return Api()
      .put("/Item/" + id + "/ToggleComplete")
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },

  async ToggleArchived(id) {
    return Api()
      .put("/Item/" + id + "/ToggleArchived")
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },
  
};
