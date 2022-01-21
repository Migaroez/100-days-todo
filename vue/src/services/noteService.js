import Api from "@/apis/todo.js";

export default {
  async Add(itemId, content) {
    return Api()
      .post("/Item/" + itemId + "/Note", content)
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
      .get("/Note", { params: id })
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },

  async Update(id, content) {
    return Api()
      .put("/Note/" + id, content)
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },
  
  async Delete(id) {
    return Api()
      .delete("/Note/" + id)
      .then(async function (response) {
        return { success: true, value: response.data };
      })
      .catch(async function (error) {
        console.error(error);
        return { success: false };
      });
  },
};
