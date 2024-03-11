using apitree.Database;
using apitree.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apitree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly DataContext db;
        public TreeController(DataContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trees = await db.Tree.Where(x => x.isDisplay == true).ToListAsync();

            var filteredTrees = new List<Tree>();
            foreach (var tree in trees)
            {
                if (!IsChildOfOtherNode(tree, trees))
                {
                    filteredTrees.Add(tree);
                }
            }

            return Ok(filteredTrees);
        }

        private bool IsChildOfOtherNode(Tree node, List<Tree> allNodes)
        {
            foreach (var otherNode in allNodes)
            {
                if (otherNode.Children != null && otherNode.Children.Any(x => x.Id == node.Id))
                {
                    return true;
                }
            }
            return false;
        }


        [HttpPost]

        public async Task<IActionResult> Post(TreeModel model)
        {
            try
            {
                if (model.parentId == null)
                {
                    var tree = new Tree()
                    {
                        title = model.title,
                        isDisplay = true,
                    };

                    db.Tree.Add(tree);
                    await db.SaveChangesAsync();

                    return Ok("Add success");
                }
                else
                {
                    var treeParent = await db.Tree.SingleOrDefaultAsync(x => x.Id == model.parentId);

                    if (treeParent == null)
                    {
                        return BadRequest("Parent node not found");
                    }

                    var tree = new Tree()
                    {
                        title = model.title,
                        isDisplay = true
                    };

                    treeParent.Children ??= new List<Tree>();
                    treeParent.Children.Add(tree);

                    db.Tree.Add(tree);
                    await db.SaveChangesAsync();

                    return Ok("Add success");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tree = await db.Tree.Include(t => t.Children).SingleOrDefaultAsync(x => x.Id == id);

                if (tree == null)
                {
                    return NotFound("Khong tim thay");
                }
                tree.isDisplay = false;
                if (tree.Children != null)
                {
                    foreach (var child in tree.Children)
                    {
                        child.isDisplay = false;
                    }
                }

                db.Update(tree);
                await db.SaveChangesAsync();

                return Ok("Xoa thanh cong");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

       

    }
}
